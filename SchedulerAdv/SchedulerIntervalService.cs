using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Command;
using CQRS.MediatR.Event;
using CQRS.MediatR.Query;
using MediatR;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using OrdersLibrary;
using Scheduler.FileService;
using Scheduler.FileService.Commands;
using Scheduler.FileService.Queries;
using Scheduler.MailService;
using Scheduler.MailService.Commands;
using Scheduler.MailService.Queries;

namespace SchedulerAdv
{
    public class SchedulerIntervalService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        private readonly IEventBus _eventBus;
        private Timer _timer;
        private string _host;
        private int _port;
        private string _from;
        private string _password;
        private int _maxMailsAtOnce;
        private int _cycleTimeMilisec;
        private string _readFilePath;
        private int _skipCounter;
        private string _writeFilePath;

        public SchedulerIntervalService(ILogger<SchedulerIntervalService> logger,
            IConfiguration configuration,
            IQueryBus queryBus,
            ICommandBus commandBus,
            IEventBus eventBus)
        {
            _logger = logger;
            _configuration = configuration;
            _queryBus = queryBus;
            _commandBus = commandBus;
            _eventBus = eventBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Orders service is starting.");
            ReadConfigurationFile();
            _skipCounter = 0;
            _timer = new Timer(RunProcess, cancellationToken, TimeSpan.Zero, TimeSpan.FromMilliseconds(_cycleTimeMilisec));
            return Task.CompletedTask;
        }

        private void ReadConfigurationFile()
        {
            _host = _configuration.GetValue<string>("Smtp:Server");
            _port = _configuration.GetValue<int>("Smtp:Port");
            _from = _configuration.GetValue<string>("Smtp:FromAddress");
            _password = _configuration.GetValue<string>("Smtp:Password");
            _maxMailsAtOnce = _configuration.GetValue<int>(key: "MaxMailsAtOnce");
            _cycleTimeMilisec = _configuration.GetValue<int>("CycleTimeMilisec");
            _readFilePath = _configuration.GetValue<string>("ReadFilePath");
            _writeFilePath = _configuration.GetValue<string>("WriteFilePath");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Orders service is stopping.");
            _timer.Dispose();
            return Task.CompletedTask;
        }

        private async void RunProcess(object state)
        {
            var cancellationToken = (CancellationToken) state;
            var ordersToSend = (await _queryBus.Send<ReadCsv, ICollection>(new ReadCsv(typeof(Order),
                _readFilePath) {Skip = _skipCounter, Take = _maxMailsAtOnce}, cancellationToken)).Cast<Order>().ToList();
            _skipCounter += _maxMailsAtOnce;
            var ordersSended = new List<Order>();
            foreach(var order in ordersToSend)
            {
                var message = await _queryBus.Send<ConvertOrderToMessage, MimeMessage>(new ConvertOrderToMessage(order), cancellationToken);
                var recepient = order?.Email;
                await _commandBus.Send(new SendMail(message, InternetAddress.Parse(_from),
                    InternetAddressList.Parse(recepient), _host, _port, _from, _password), cancellationToken);
                ordersSended.Add(order);
            }
            
            await _commandBus.Send(new SaveToCsv(_writeFilePath, ordersSended), cancellationToken);
        }
    }
}

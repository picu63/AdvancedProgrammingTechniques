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

namespace SchedulerAdv
{
    public class SchedulerIntervalService : IHostedService
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;
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
        private string _filePath;

        public SchedulerIntervalService(ILogger<SchedulerIntervalService> logger,
            IConfiguration configuration,
            IQueryBus queryBus,
            ICommandBus commandBus,
            IEventBus eventBus)
        {
            _logger = logger;
            _config = configuration;
            _queryBus = queryBus;
            _commandBus = commandBus;
            _eventBus = eventBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Orders service is starting.");
            _host = _config.GetValue<string>("Smtp:Server");
            _port = _config.GetValue<int>("Smtp:Port");
            _from = _config.GetValue<string>("Smtp:FromAddress");
            _password = _config.GetValue<string>("Smtp:Password");
            _maxMailsAtOnce = _config.GetValue<int>(key: "MaxMailsAtOnce");
            _cycleTimeMilisec = _config.GetValue<int>("CycleTimeMilisec");
            _filePath = _config.GetValue<string>("CsvFilePath");

            _timer = new Timer(RunProcess, cancellationToken, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Orders service is stopping.");
            _timer.Dispose();
            return Task.CompletedTask;
        }

        private async void RunProcess(object state)
        {
            if (state is CancellationToken)
            {
                _logger.LogInformation("state jest cancellation tokenem");
            }
            const string writePath = "C:\\Users\\picu6\\source\\repos\\ZTP\\SchedulerAdv\\csv_file.csv";
            var collection = await _queryBus.Send<ReadCsv, ICollection>(new ReadCsv(typeof(Order),
                _filePath) {Skip = 100, Take = 100});
            foreach (var o in collection)
            {
                var order = o as Order;
                var message = await _queryBus.Send<ConvertOrderToMessage, MimeMessage>(new ConvertOrderToMessage(order));
                var recepient = order.Email;
                await _commandBus.Send(new SendMail(message, InternetAddress.Parse(_from),
                    InternetAddressList.Parse(recepient), _host, _port, _from, _password));
            }

            await _commandBus.Send(new SaveToCsv(writePath, collection));
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using SchedulerAkka.FileService;
using SchedulerAkka.MailService;
using Order = SchedulerAkka.OrdersLibrary.Order;

namespace SchedulerAkka
{
    public class SchedulerIntervalService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;
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
        private ActorSystem _system;
        private IActorRef _messageSenderReceiveActor;
        private IActorRef _dataReaderReceiveActor;
        private IActorRef _dataWriterReceiveActor;
        private IActorRef _messageConverterReceiveActor;

        public SchedulerIntervalService(ILogger<SchedulerIntervalService> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Orders service is starting.");
            ReadConfigurationFile();
            _skipCounter = 0;
            _system = ActorSystem.Create("SchedulerAkka");
            _messageSenderReceiveActor = _system.ActorOf<MessageSenderReceiveActor>();
            _dataReaderReceiveActor = _system.ActorOf<DataReaderReceiveActor>();
            _dataWriterReceiveActor = _system.ActorOf<DataWriterReceiveActor>();
            _messageConverterReceiveActor = _system.ActorOf<MessageConverterReceiveActor>();
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
            var cancellationToken = (CancellationToken)state;
            var ordersToSend = (await _dataReaderReceiveActor
                .Ask<List<object>>(new RecordsReaderMessage(_readFilePath, typeof(Order)){Skip = _skipCounter, Take = _maxMailsAtOnce}, cancellationToken))
                .Cast<Order>();
            foreach (var order in ordersToSend)
            {
                var message = await _messageConverterReceiveActor
                    .Ask<MimeMessage>(order);
                Console.WriteLine(await _messageSenderReceiveActor
                    .Ask(new SendEmailMessage(message,
                        InternetAddress.Parse(_from),
                        InternetAddressList.Parse(order.Email),
                        _host,
                        _port,
                        _from,
                        _password)));
            }
            await _dataWriterReceiveActor
                .Ask(new RecordsWriterMessage(_writeFilePath, ordersToSend.ToList()));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using DataProvider;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using Scheduler.Models;

namespace Scheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly ISmtpClient _smtpClient;
        public CsvReader CsvReader { get; private set; }

        private int maxMailsAtOnce;
        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _smtpClient = new SmtpClient();
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Reading application settings...");
            var (host, port, from, password) = GetSmtpFromConfiguration();
            this.maxMailsAtOnce = _config.GetValue<int>(key: "MaxMailsAtOnce");

            _logger.LogInformation("Initializing data reader...");
            InitializeDataReader();

            _logger.LogInformation("Connecting to the smtp server...");
            await ConnectToSmtpAsync(host, port, from, password, cancellationToken);

            await base.StartAsync(cancellationToken);
        }


        private void InitializeDataReader()
        {
            var filePath = _config.GetValue<string>("CsvFilePath");
            var sr = new StreamReader(filePath);
            CsvReader = new CsvReader(sr, CultureInfo.InvariantCulture);
            CsvReader.Configuration.HasHeaderRecord = false;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _smtpClient.DisconnectAsync(false, cancellationToken);
            await base.StopAsync(cancellationToken);
        }

        private async Task ConnectToSmtpAsync(string host, int port, string from, string password, CancellationToken cancellationToken)
        {
            await _smtpClient.ConnectAsync(host, port, false, cancellationToken);
            await _smtpClient.AuthenticateAsync(from, password, cancellationToken);
        }
        
        private (string host, int port, string from, string password) GetSmtpFromConfiguration()
        {
            var host = _config.GetValue<string>("Smtp:Server");
            var port = _config.GetValue<int>("Smtp:Port");
            var from = _config.GetValue<string>("Smtp:FromAddress");
            var password = _config.GetValue<string>("Smtp:Password");
            return (host, port, from, password);
        }

        /// <summary>
        /// Process wykonuj¹cy siê ci¹gle.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Starting cycle: {DateTimeOffset.Now}");
                _logger.LogInformation("Getting data from file...");
                var orders = CsvReader.GetRecords<Order>().Take(maxMailsAtOnce).ToList();
                if(!orders.Any())
                {
                    await WaitAsync(60000, stoppingToken);
                    continue;
                }
                _logger.LogInformation($"Found {orders.Count()} orders for send.");
                _logger.LogInformation("Starting sending process...");
                foreach (var order in orders)
                {
                    var message = OrderService.CreateMessage(order);
                    _logger.LogInformation($"Sending message: {order}");
                    await _smtpClient.SendAsync(message, stoppingToken);
                }
                await WaitAsync(60000, stoppingToken);
            }
        }

        private async Task WaitAsync(int milisecondsDelay, CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waiting 60 seconds for another cycle...");
            await Task.Delay(milisecondsDelay, stoppingToken);
        }
    }
}

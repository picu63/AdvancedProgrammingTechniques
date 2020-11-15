using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using DataProvider;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using OrderService;
using OrderService.Models;

namespace Scheduler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly ISmtpClient _smtpClient;
        private IReader Reader { get; set; }

        private int _maxMailsAtOnce;
        private int _cycleTimeMilisec;
        private string _sender;
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
            this._maxMailsAtOnce = _config.GetValue<int>(key: "MaxMailsAtOnce");
            this._cycleTimeMilisec = _config.GetValue<int>("CycleTimeMilisec");
            this._sender = from;
            _logger.LogInformation("Initializing data reader...");
            InitializeDataReader();

            _logger.LogInformation("Connecting to the smtp server...");
            await ConnectToSmtpAsync(host, port, from, password, cancellationToken);

            await base.StartAsync(cancellationToken);
        }


        #region StartAsync Methods
        private void InitializeDataReader()
        {
            var filePath = _config.GetValue<string>("CsvFilePath");
            var sr = new StreamReader(filePath);
            Reader = new CsvReader(sr, CultureInfo.InvariantCulture);
            Reader.Configuration.HasHeaderRecord = false;
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
        #endregion


        /// <summary>
        /// Service running in cycle.
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogError($"Starting cycle: {DateTimeOffset.Now}");
                await ExecuteOrderProcessAsync(stoppingToken);
                await WaitAsync(_cycleTimeMilisec, stoppingToken);
            }
        }


        #region ExecuteAsync Methods
        private async Task ExecuteOrderProcessAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Getting data from file...");
            var ordersProvider = new OrdersProvider();
            var orders = ordersProvider.GetOrdersFromCsv(Reader).Take(_maxMailsAtOnce).ToList();

            if (!orders.Any()) return;

            _logger.LogInformation($"Found {orders.Count} orders for send.");
            _logger.LogInformation("Starting sending process...");
            var orderMailService = new OrderMailService(_smtpClient, _logger);
            await orderMailService.SendOrders(orders, stoppingToken);
        }
        #endregion


        /// <summary>
        /// Methods for end of 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Disconecting from smtp server...");
            await _smtpClient.DisconnectAsync(false, cancellationToken);
            await base.StopAsync(cancellationToken);
        }



        private async Task WaitAsync(int milisecondsDelay, CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Waiting {milisecondsDelay} miliseconds for another cycle...");
            await Task.Delay(milisecondsDelay, stoppingToken);
        }
    }
}

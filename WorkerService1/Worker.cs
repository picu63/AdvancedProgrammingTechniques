using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataProvider;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using WorkerService1.Models;

namespace WorkerService1
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _config;
        private readonly ISmtpClient _smtpClient;
        private readonly ICsvService _csvService;
        public Worker(ILogger<Worker> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _smtpClient = new SmtpClient();
            _csvService = new CsvService();
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Reading configuration");
            var (host, port, from, password) = GetSmtpFromConfiguration();

            _logger.LogInformation("Connecting to the smtp server");
            await ConnectToSmtpAsync(host, port, from, password, cancellationToken);

            await base.StartAsync(cancellationToken);
        }

        private async Task ConnectToSmtpAsync(string host, int port, string @from, string password, CancellationToken cancellationToken)
        {
            await _smtpClient.ConnectAsync(host, port, false, cancellationToken);
            await _smtpClient.AuthenticateAsync(@from, password, cancellationToken);
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
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //TODO odczytywanie danych z pliku csv
                //TODO foreach do ka¿dego z Order i wys³anie max 100
                //foreach (var order in orders)
                //{
                var message = CreateOrderMessage(new Order());
                await _smtpClient.SendAsync(message, stoppingToken);
                //}
                await Task.Delay(5000, stoppingToken); //TODO Wykonywanie raz na minutê
            }
        }

        //TODO Przenieœæ do klasy np OrderService i poprawiæ
        private static MimeMessage CreateOrderMessage(Order order)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("mina97@ethereal.email"));
            message.To.Add(MailboxAddress.Parse("mina97@ethereal.email"));
            message.Subject = "How you doin?";
            message.Body = new TextPart("body") {Text = "To jest przyk³adowy tekst"};
            return message;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using OrderLibrary.Models;

namespace OrderLibrary
{
    public class OrderMailService
    {
        private readonly ILogger _logger;
        private readonly ISmtpClient _smtpClient;

        public OrderMailService(ISmtpClient smtpClient)
        {
            this._smtpClient = smtpClient;
        }

        public OrderMailService(ISmtpClient smtpClient, ILogger logger)
        {
            this._logger = logger;
            this._smtpClient = smtpClient;
        }

        /// <summary>
        /// Send orders via smtp client.
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public async Task SendOrders(IEnumerable<Order> orders, CancellationToken stoppingToken)
        {
            foreach (var order in orders)
            {
                _logger?.LogInformation($"Creating order message to client: {order.Email}.");
                var message = CreateOrderMessage(order, MailboxAddress.Parse(order.Email));
                await SendOrder(stoppingToken, order, message);
            }
        }

        private async Task SendOrder(CancellationToken stoppingToken, Order order, MimeMessage message)
        {
            try
            {
                _logger?.LogInformation($"Sending message: {order}");
                await _smtpClient.SendAsync(message, stoppingToken);
            }
            catch (ServiceNotConnectedException ex)
            {
                this._logger?.Log(LogLevel.Error, $"Sending order error {ex}.");
                throw;
            }
        }

        private static MimeMessage CreateOrderMessage(Order order, InternetAddress from)
        {
            var message = new MimeMessage();
            message.From.Add(from);
            message.To.Add(MailboxAddress.Parse(order.Email));
            message.Subject = $"Order from the best company! {order.NrZamowienia}";
            message.Body = new BodyBuilder()
                {
                    TextBody = $"Dear {order.Imie} {order.Nazwisko} your order number: {order.NumerPaczki}" +
                               $" is on way to {order.AdresZamowienia}, thanks for buying."
                }
                .ToMessageBody();
            return message;
        }

        private static MimeMessage CreateOrderMessageWithLogger(Order order, MailboxAddress from, ILogger logger)
        {

            var message = new MimeMessage();
            message.From.Add(from);
            message.To.Add(MailboxAddress.Parse(order.Email));
            message.Subject = "Order from the best company!";
            message.Body = new BodyBuilder()
                {
                    TextBody = $"Dear {order.Imie} {order.Nazwisko} your order number: {order.NumerPaczki}" +
                               $" is on way to {order.AdresZamowienia}, thanks for buying."
                }
                .ToMessageBody();
            return message;
        }
    }
}

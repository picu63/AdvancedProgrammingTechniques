using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Smtp;
using FluentEmail.Razor;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using MailKit.Net.Smtp;
using ZTP.Scheduler.Models;

namespace ZTP.Scheduler
{
    public class SmtpService
    {
        public SmtpService(string userName, string password, string host, int port, bool useSsl = false)
        {
            this.userName = userName;
            this.password = password;
            this.host = host;
            this.port = port;
            this.useSsl = useSsl;
        }

        private readonly string userName;
        private string password;
        private string host;
        private int port;
        private bool useSsl;

        /// <summary>
        /// Sends orders by  given collection.
        /// </summary>
        /// <param name="orders"></param>
        /// <returns>Orders sent.</returns>
        public async Task<ICollection<Order>> SendOrders(IEnumerable<Order> orders)
        {
            try
            {
                var ordersSent = new ConcurrentBag<Order>();
                var tasks = new ConcurrentBag<Task>();

                foreach (var order in orders)
                {
                    var message = CreateOrderMessage(order);
                    tasks.Add(this.SendAsync(message).ContinueWith((t) =>
                    {
                        if (t.Result)
                        {
                            Console.WriteLine($"Wysłano {order}");
                            ordersSent.Add(order); 
                        }
                    }));
                }

                Task.WaitAll(tasks.ToArray());
                return ordersSent.ToArray();
            }
            catch (Exception ex)
            {
                //logger
                throw;
            }
        }

        private static MimeMessage CreateOrderMessage(Order order)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(Encoding.UTF8,"Sender Name", "sender@gmail.com" ));
            message.To.Add(new MailboxAddress("Recipient Name","recipient@gmail.com"));
            message.Subject = "New order";
            BodyBuilder body = new BodyBuilder();
            body.TextBody = $@"You have one new order
{order}";
            body.HtmlBody = $@"<h1><b>You have one new order</b></h1>
<p>Order ID: {order.NrZamowienia}</p>
<p>{order.Imie}</p>";
            message.Body = body.ToMessageBody();
            return message;
        }

        private async Task<bool> SendAsync(MimeMessage message)
        {
            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.ethereal.email", 587, false);
                    await client.AuthenticateAsync("mina97@ethereal.email", "9gwZB43UAeBThEJV8X");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                //logger
                return false;
            }
        }
    }
}

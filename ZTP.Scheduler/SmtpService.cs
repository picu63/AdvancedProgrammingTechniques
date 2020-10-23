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
    public static class SmtpService
    {
        public static void SendTemplate()
        {
			var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
            message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
            message.To.Add(new MailboxAddress("Pietrek Olearczyk", "polearczyk@jantar.pl"));
            message.Subject = "How you doin'?";

            message.Body = new TextPart("plain")
            {
                Text = @"Hey Chandler,

I just wanted to let you know that Monica and I were going to go play some paintball, you in?

-- Joey"
            };

            SendAsync(message);
		}

        /// <summary>
        /// Sends orders by  given collection.
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="count"></param>
        /// <returns>Orders sent.</returns>
        public static IEnumerable<Order> SendOrders(List<Order> orders)
        {
            var ordersBag = new ConcurrentBag<Order>();
            var tasks = new ConcurrentBag<Task>();
            foreach (var order in orders)
            {
                var message = CreateOrderMessage(order);
                tasks.Add(SendAsync(message).ContinueWith((t) =>
                {
                    if (t.Result)
                    {
                        Console.WriteLine($"Wysłano {order}");
                        ordersBag.Add(order);
                    }
                }, TaskContinuationOptions.OnlyOnRanToCompletion));
            }

            Task.WaitAll(tasks.ToArray());
            return ordersBag;
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
<p>Id: {order.NrZamowienia}</p>
<p></p>";
            message.Body = body.ToMessageBody();
            return message;
        }

        public static async Task<bool> SendAsync(MimeMessage message)
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

                return false;
            }
        }
        
    }
}

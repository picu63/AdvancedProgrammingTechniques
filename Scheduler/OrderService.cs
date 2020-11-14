using System;
using System.Collections.Generic;
using System.Text;
using DataProvider;
using MimeKit;
using Scheduler.Models;

namespace Scheduler
{
    public static class OrderService
    {
        //TODO Poprawić tworzenie wiadomości o zamówieniu uzupełnić Subject i Body
        public static MimeMessage CreateMessage(Order order)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse("mina97@ethereal.email"));
            message.To.Add(MailboxAddress.Parse("mina97@ethereal.email"));
            message.Subject = "Order from best company!";
            message.Body = new TextPart("body")
            {
                Text = $"Dear {order.Imie} {order.Nazwisko} your order number:" +
                $"{order.NumerPaczki} is on way to {order.AdresZamowienia}, thanks for buying."
            };
            return message;
        }
    }
}

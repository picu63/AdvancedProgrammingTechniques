using System;
using System.Collections.Generic;
using System.Text;
using ZTP.Scheduler.Models;
using FluentMailer;
using FluentMailer.Interfaces;
using System.Runtime.CompilerServices;
using FluentMailer.Factory;

namespace ZTP.Scheduler.Services
{
    public class MailService : FluentMailerService.FluentMailerService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orders"></param>
        public void SendOrders(List<Order> orders)
        {
            foreach (var order in orders)
            {
                CreateMessage().WithViewBody(CreateOrderViewBody(order))
                       .WithSubject($"Zamówienie nr. {order.Email}")
                       .WithReceiver("picu63@gmail.com")
                       .Send();
            }

        }
        private string CreateOrderViewBody(Order order)
        {
            return $@"<body>
    <h1>Nowe zamówienie:</h1>
    <p>Id zamówienia: {order.Id}</p>
    <p>Imię: {order.Imie}</p>
    <p>Nazwisko: {order.Nazwisko}</p>
    <p>Nr. zamówienia: {order.NrZamowienia}</p>
    <p>Nazwa produktu: {order.Produkt}</p>
    <p>Cena: {order.Cena}</p>
    <p>Ilość: {order.Ilosc}</p>
    <p><br>Zamówienie przyjął: {order.AdresPrzyjmujacego}</p>
</body>";
        }
    }
}

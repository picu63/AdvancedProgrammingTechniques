using FluentMailer.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using ZTP.Scheduler.Models;
using ZTP.Scheduler.Services;

namespace ZTP.Scheduler.Tests
{
    public class MailServiceTests
    {
        [Test]
        public void SendOneMail()
        {
            MailService mailService = new MailService();
            mailService.SendOrders(new List<Order>() 
            { 
                new Order() 
                { 
                    Id = 1, 
                    AdresPrzyjmujacego = "Kolorowa 14", 
                    Cena = 19, 
                    Email = "picu63@gmail.com", 
                    Ilosc = 2, Imie = "Piotr", 
                    Nazwisko = "Olearczyk", 
                    NrZamowienia = 142,
                    Produkt = "Kalesony Mêskie"
                } 
            });
        }
    }
}
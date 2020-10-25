using CsvHelper;
using NLog.Targets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using Org.BouncyCastle.Asn1.Cms;
using ZTP.Scheduler.Models;

namespace ZTP.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadConfiguration();

            RunService();

            Console.WriteLine("Ukończono wysyłanie wszystkich zamówień.");
            Console.ReadKey();
        }

        private static void LoadConfiguration()
        {
            var configServices = new ConfigServices();
            var configFile = configServices.Get<ConfigFile>(GlobalConfig.ConfigPath);
            GlobalConfig.CsvFile = configFile.CsvFile;
            GlobalConfig.Smtp.UserName = configFile.Smtp.UserName;
            GlobalConfig.Smtp.Host = configFile.Smtp.Host;
            GlobalConfig.Smtp.Password = configFile.Smtp.Password;
            GlobalConfig.Smtp.Port = configFile.Smtp.Port;
        }

        private static void RunService()
        {
            while (true)
            {

                string filePath = GlobalConfig.CsvFile;
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException();
                }
                var ordersLeft = new List<Order>();
                ordersLeft =
                    (List<Order>)DataService.ReadFromCsvFile<Order>(filePath,
                        CultureInfo.InvariantCulture);
                do
                {
                    var ordersToSent = (ordersLeft.Count >= 100)
                        ? ordersLeft.Take(100).ToList()
                        : ordersLeft.Take(ordersLeft.Count).ToList();
                    var smtpService = new SmtpService(GlobalConfig.Smtp.UserName, GlobalConfig.Smtp.Password,
                        GlobalConfig.Smtp.Host, GlobalConfig.Smtp.Port);
                    var ordersSent = smtpService.SendOrders(ordersToSent.ToList()).Result;
                    ordersLeft = ordersLeft.Except(ordersSent).ToList();
                    if (!ordersLeft.Any())
                    {
                        ordersLeft = null;
                    }
                    else
                    {
                        Thread.Sleep(10000);
                    }
                } while (ordersLeft != null);
            }
        }
    }
}

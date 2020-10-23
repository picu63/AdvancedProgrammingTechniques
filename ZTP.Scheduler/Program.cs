using CsvHelper;
using NLog.Targets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using ZTP.Scheduler.Models;
using FluentMailer;
using FluentMailer.Interfaces;
using CsvHelper.Configuration;
using ZTP.Scheduler.Services;

namespace ZTP.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory()+ Path.DirectorySeparatorChar + "zamowienia.csv");
            //1. Odczytać dane z pliku csv
            using (var streamReader = new StreamReader(filePath))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                var orders = csvReader.GetRecords<Order>().ToList();
                var mailService = new MailService();
                mailService.SendOrders(orders);
            }
        }
    }
}

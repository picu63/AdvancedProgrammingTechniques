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
using FluentEmail.Core;
using Microsoft.Extensions.DependencyInjection;
using ZTP.Scheduler.Models;

namespace ZTP.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var filePath = string.Empty;
            if (args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                Console.WriteLine("Brak ścieżki do pliku. Podaj ścieżkę do pliku z mailami.");
                filePath = Console.ReadLine();
            }
            else
            {
                filePath = args[0];
            }
            
            List<Order> orders;
            //1. Odczytać dane z pliku csv
            using (var streamReader = new StreamReader(filePath))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                csvReader.Configuration.HasHeaderRecord = false;
                orders = csvReader.GetRecords<Order>().ToList();
            }
            var sended = SmtpService.SendOrders(orders).ToList();
        }
    }
}

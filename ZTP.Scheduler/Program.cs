using CsvHelper;
using NLog.Targets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using DataProvider;
using ZTP.Scheduler.Models;

namespace ZTP.Scheduler
{
    class Program
    {
        private ICsvService csvService;
        static void Main(string[] args)
        {
            try
            {
                RunService();
            }
            catch (Exception ex)
            {
                //logger
                throw;
            }
        }

        private static void RunService()
        {
            while (true)
            {

                //string filePath = ConfigurationManager.AppSettings.Get("CsvFilePath");
                //if (!File.Exists(filePath))
                //{
                //    throw new FileNotFoundException();
                //}

                //var ordersLeft = csvService.ReadCsvToModel<Order>(ConfigurationManager.AppSettings["CsvFilePath"]);
                //do
                //{
                //    var ordersToSent = (ordersLeft.Count >= 100)
                //        ? ordersLeft.Take(100).ToList()
                //        : ordersLeft.Take(ordersLeft.Count).ToList();
                //    var ordersSent = smtpService.SendOrders(ordersToSent.ToList()).Result;
                //    ordersLeft = ordersLeft.Except(ordersSent).ToList();
                //    if (!ordersLeft.Any())
                //    {
                //        ordersLeft = null;
                //    }
                //    else
                //    {
                //        Thread.Sleep(10000);
                //    }
                //} while (ordersLeft != null);

                //if (File.Exists(filePath))
                //{
                //    File.Delete(filePath);
                //}
            }
        }
    }
}

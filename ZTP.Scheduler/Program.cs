using CsvHelper;
using NLog.Targets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ZTP.Scheduler.Models;

namespace ZTP.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "";
            //1. Odczytać dane z pliku csv
            using (StreamReader streamReader = new StreamReader(filePath))
            using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                List<Order> orders = csvReader.GetRecords<Order>().ToList();
            }
            
        }
    }
}

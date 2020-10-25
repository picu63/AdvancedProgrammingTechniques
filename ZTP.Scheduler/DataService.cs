using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using ZTP.Scheduler.Models;

namespace ZTP.Scheduler
{
    internal class DataService
    {
        internal static ICollection<T> ReadFromCsvFile<T>(string filePath, CultureInfo cultureInfo, bool hasHeaderRecord = false)
        {
            using (var streamReader = new StreamReader(filePath))
            using (var csvReader = new CsvReader(streamReader, cultureInfo))
            {
                csvReader.Configuration.HasHeaderRecord = hasHeaderRecord;
                return csvReader.GetRecords<T>().ToList();
            }
        }
    }
}

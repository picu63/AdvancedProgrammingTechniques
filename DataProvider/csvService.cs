using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace DataProvider
{
    public class CsvService
    {
        private static CsvService csvService;
        public static CsvService GetInstance()
        {
            if (csvService == null)
            {
                csvService = new CsvService();
                return csvService;
            }
            return csvService;
        }

        public CsvReader CsvReader { get; private set; }
        public IEnumerable<T> ReadCsvToModels<T>(string path)
        {
            var streamReader = new StreamReader(path);
            CsvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            return CsvReader.GetRecords<T>();
        }
    }
}

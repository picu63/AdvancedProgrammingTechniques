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
    public class CsvService : ICsvReader
    {
        public CsvService(string csvPath)
        {
            var streamReader = new StreamReader(csvPath);
            CsvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
        }
        public CsvReader CsvReader { get; private set; }

        public IEnumerable<T> ReadCsvToModels<T>(string csvPath)
        {
            return CsvReader.GetRecords<T>();
        }
    }
}

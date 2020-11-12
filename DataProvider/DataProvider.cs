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
    public class DataProvider : ICsvReader
    {
        public DataProvider(string filePath)
        {
            _streamReader = new StreamReader(filePath);
            CsvReader = new CsvReader(_streamReader, CultureInfo.CurrentCulture);
        }

        private readonly StreamReader _streamReader;
        public CsvReader CsvReader { get; }

        public IEnumerable<T> ReadCsvToModels<T>(string csvPath)
        {
            return CsvReader.GetRecords<T>();
        }
    }
}

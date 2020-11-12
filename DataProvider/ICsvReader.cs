using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataProvider
{
    public interface ICsvReader
    {
        IEnumerable<T> ReadCsvToModels<T>(string csvPath);
    }
}

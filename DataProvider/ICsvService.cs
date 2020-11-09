using System;
using System.Collections.Generic;
using System.Text;

namespace DataProvider
{
    public interface ICsvService
    {
        IEnumerable<T> ReadCsvToModel<T>(string csvPath);
    }
}

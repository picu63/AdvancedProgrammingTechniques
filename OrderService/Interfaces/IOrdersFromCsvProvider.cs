using System;
using System.Collections.Generic;
using System.Text;
using CsvHelper;
using OrderLibrary.Models;

namespace OrderLibrary
{
    public interface IOrdersFromCsvProvider
    {
        IEnumerable<Order> GetOrdersFromCsv(IReader reader);
        IAsyncEnumerable<Order> GetOrdersFromCsvAsync(IReader reader);
    }
}

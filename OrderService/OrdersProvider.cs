using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using OrderService.Models;

namespace OrderService
{
    public class OrdersProvider : IOrdersCsvProvider
    {
        public IEnumerable<Order> GetOrdersFromCsv(IReader reader)
        {
            return reader.GetRecords<Order>();
        }
        public IAsyncEnumerable<Order> GetOrdersFromCsvAsync(IReader reader)
        {
            return reader.GetRecordsAsync<Order>();
        }
    }
}

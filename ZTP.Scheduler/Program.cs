using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ZTP.Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = CsvHelper("filepath");
            SendEmail(orders);
        }
    }
}

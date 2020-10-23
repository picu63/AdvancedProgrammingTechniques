using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTP.Scheduler.Framework
{
    class Program
    {
        static void Main(string[] args)
        {
            FluentService fluentService = new FluentService();
            var message = fluentService.CreateMessage().WithViewBody($"To jest przykładowy string");
            message.WithReceiver("picu63@gmail.com").Send();

        }
    }
}

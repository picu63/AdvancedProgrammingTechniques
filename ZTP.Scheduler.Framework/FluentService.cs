using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMailer.Factory;
using FluentMailer.Interfaces;

namespace ZTP.Scheduler.Framework
{
    public class FluentService : IFluentMailer
    {
        public IFluentMailerMessageBodyCreator CreateMessage()
        {
            return FluentMailerFactory.Create().CreateMessage();
        }
    }
}

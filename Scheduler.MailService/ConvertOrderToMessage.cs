using System;
using System.Collections.Generic;
using System.Text;
using CQRS.MediatR.Command;
using CQRS.MediatR.Query;
using MimeKit;
using OrdersLibrary;

namespace Scheduler.MailService
{
    public class ConvertOrderToMessage: IQuery<MimeMessage>
    {
        public Order Order { get; }

        public ConvertOrderToMessage(Order order)
        {
            Order = order;
        }
    }
}

using CQRS.MediatR.Query;
using MimeKit;
using OrdersLibrary;

namespace Scheduler.MailService.Queries
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

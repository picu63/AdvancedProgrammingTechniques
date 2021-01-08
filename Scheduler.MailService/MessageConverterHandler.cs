using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Query;
using MimeKit;

namespace Scheduler.MailService
{
    public class MessageConverterHandler: IQueryHandler<ConvertOrderToMessage, MimeMessage>
    {
        public async Task<MimeMessage> Handle(ConvertOrderToMessage request, CancellationToken cancellationToken)
        {
            var order = request.Order;
            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(order.Email));
            message.Subject = $"Order from the best company! {order.NrZamowienia}";
            message.Body =  new BodyBuilder()
                {
                    TextBody = $"Dear {order.Imie} {order.Nazwisko} your order number: {order.NumerPaczki}" +
                               $" is on way to {order.AdresZamowienia}, thanks for buying."
                }
                .ToMessageBody();
            return message;
        }
    }
}

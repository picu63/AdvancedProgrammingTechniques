using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using MimeKit;
using SchedulerAkka.OrdersLibrary;

namespace SchedulerAkka.MailService
{
    public class MessageConverterReceiveActor: ReceiveActor
    {
        public MessageConverterReceiveActor()
        {
            Receive<Order>(Handle);
        }

        private void Handle(Order order)
        {
            var message = new MimeMessage();
            message.To.Add(MailboxAddress.Parse(order.Email));
            message.Subject = $"Order from the best company! {order.NrZamowienia}";
            message.Body =  new BodyBuilder()
                {
                    TextBody = $"Dear {order.Imie} {order.Nazwisko} your order number: {order.NumerPaczki}" +
                               $" is on way to {order.AdresZamowienia}, thanks for buying."
                }
                .ToMessageBody();
            Sender.Tell(message);
        }
    }

    public class OrderMessageProps
    {
        public OrderMessageProps(Order order)
        {
            Order = order;
        }

        public Order Order { get; set; }
    }
}

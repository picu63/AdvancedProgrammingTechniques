using CQRS.MediatR.Event;

namespace Scheduler.MailService.Events
{
    public class MailHasBeenSent: IEvent
    {
        public string From { get; }
        public string Recipient { get; }
        public string Title { get; }

        public MailHasBeenSent(string from, string recipient, string title)
        {
            From = @from;
            Recipient = recipient;
            Title = title;
        }
    }
}

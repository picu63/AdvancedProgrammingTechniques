using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Event;
using Microsoft.Extensions.Logging;

namespace Scheduler.MailService.Events
{
    public class MailEventHandler: IEventHandler<MailHasBeenSent>
    {
        private readonly ILogger<MailEventHandler> _logger;

        public MailEventHandler(ILogger<MailEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(MailHasBeenSent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Mail with title \"{notification.Title}\" was sent to {notification.Recipient} from {notification.From}.");
            return Task.CompletedTask;
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Command;
using CQRS.MediatR.Event;
using MailKit.Net.Smtp;
using MediatR;
using MimeKit;
using Scheduler.MailService.Events;

namespace Scheduler.MailService.Commands
{
    public class SendEmailHandler: ICommandHandler<SendMail>
    {
        private readonly IEventBus _eventBus;

        public SendEmailHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task<Unit> Handle(SendMail request, CancellationToken cancellationToken)
        {
            var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(request.Host, request.Port, cancellationToken: cancellationToken);
            await smtpClient.AuthenticateAsync(request.UserName, request.Password, cancellationToken);
            var message = request.Message;
            message.Sender = MailboxAddress.Parse(request.UserName);
            await smtpClient.SendAsync(request.Message, cancellationToken);
            await _eventBus.Publish(new MailHasBeenSent(request.From.ToString(), request.To.ToString(),
                request.Message.Subject),cancellationToken);
            smtpClient.Dispose();
            return Unit.Value;
        }
    }
}
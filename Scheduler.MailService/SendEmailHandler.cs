using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Command;
using MailKit.Net.Smtp;
using MediatR;
using MimeKit;

namespace Scheduler.MailService
{
    public class SendEmailHandler: ICommandHandler<SendMail>
    {
        public async Task<Unit> Handle(SendMail request, CancellationToken cancellationToken)
        {
            SmtpClient smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(request.Host, request.Port, cancellationToken: cancellationToken);
            await smtpClient.AuthenticateAsync(request.UserName, request.Password, cancellationToken);
            var message = request.Message;
            message.Sender = MailboxAddress.Parse(request.UserName);
            await smtpClient.SendAsync(request.Message, cancellationToken);
            smtpClient.Dispose();

            return Unit.Value;
        }
    }
}

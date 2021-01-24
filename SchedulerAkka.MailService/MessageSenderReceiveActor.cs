using System;
using Akka.Actor;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace SchedulerAkka.MailService
{
    public class MessageSenderReceiveActor : ReceiveActor
    {
        public MessageSenderReceiveActor()
        {
            Receive<SendEmailMessage>(Handle);
        }

        private void Handle(SendEmailMessage request)
        {
            var smtpClient = new SmtpClient();
            smtpClient.Connect(request.Host, request.Port);
            smtpClient.Authenticate(request.UserName, request.Password);
            var connected = smtpClient.IsConnected;
            var message = request.Message;
            message.Sender = MailboxAddress.Parse(request.UserName);
            smtpClient.Send(request.Message);
            Sender.Tell($"Mail with title \"{request.Message.Subject}\" was sent to {request.Message.To} from {request.From}.");
        }
    }
}
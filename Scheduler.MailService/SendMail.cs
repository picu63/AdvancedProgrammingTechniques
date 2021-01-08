using System;
using CQRS.MediatR.Command;
using MailKit.Net.Smtp;
using MimeKit;

namespace Scheduler.MailService
{
    public class SendMail : ICommand
    {
        public MimeMessage Message { get; }
        public InternetAddress From { get; }
        public InternetAddressList To { get; }
        public string Host { get; }
        public int Port { get; }
        public string UserName { get; }
        public string Password { get; }

        public SendMail(MimeMessage message, InternetAddress from, InternetAddressList to, string host, int port, string userName, string password)
        {
            Message = message;
            From = @from;
            To = to;
            Host = host;
            Port = port;
            UserName = userName;
            Password = password;
        }
    }
}

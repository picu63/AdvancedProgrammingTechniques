using System;
using System.Collections.Generic;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace MailService
{
    interface ISmtpClient
    {
        SmtpClient SmtpClient { get; }
        void SendEmail(MimeMessage mailMessage);
    }
}

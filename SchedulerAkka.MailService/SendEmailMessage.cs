using MimeKit;

namespace SchedulerAkka.MailService
{
    public class SendEmailMessage
    {
        public MimeMessage Message { get; }
        public InternetAddress From { get; }
        public InternetAddressList To { get; }
        public string Host { get; }
        public int Port { get; }
        public string UserName { get; }
        public string Password { get; }

        public SendEmailMessage(MimeMessage message, InternetAddress from, InternetAddressList to, string host, int port, string userName, string password)
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
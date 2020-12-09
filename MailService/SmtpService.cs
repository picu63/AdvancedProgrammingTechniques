using System;
using MimeKit;
using MailKit.Net.Smtp;

namespace MailService
{
    public class SmtpService : ISmtpClient
    {
        /// <summary>
        /// Klient Smtp.
        /// </summary>
        public SmtpClient SmtpClient { get; }

        /// <summary>
        /// Tworzy serwis do wysyłania maili przez serwer SMTP.
        /// </summary>
        /// <param name="userLogin">Adres z którego wysyłany jest mail oraz jednocześnie nazwa użytkownika do serwera SMTP.</param>
        /// <param name="password">Hasło do serwera SMTP.</param>
        /// <param name="host">Host.</param>
        /// <param name="port">Port do serwera Smtp.</param>
        /// <param name="enableSsl">Jeśli true to szyfrowanie SSL po TLS.</param>
        public SmtpService(string userLogin, string password, string host, int port, bool enableSsl = false)
        {
            this.SmtpClient = new SmtpClient {Timeout = 5000};
            this.SmtpClient.Connect(host, port);
            this.SmtpClient.Authenticate(userLogin, password);
        }

        /// <summary>
        /// Wysyłanie maila.
        /// </summary>
        /// <param name="mailMessage">Wiadomość mail.</param>
        public void SendEmail(MimeMessage mailMessage)
        {
            SmtpClient.Send(mailMessage);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ZTP.Scheduler.Models
{
    [Serializable]
    public class ConfigFile
    {

        public Smtp Smtp { get; set; }

        /// <summary>
        /// Ścieżka katalogu z załącznikami do wysłania.
        /// </summary>
        public string CsvFile { get; set; }
    }

    public class Smtp
    {
        /// <summary>
        /// Nazwa użytkownika/mail użytkownika do serwera SMTP
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Hasło do serwera SMTP
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Port serwera.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Host servera SMTP.
        /// </summary>
        public string Host { get; set; }
    }
}

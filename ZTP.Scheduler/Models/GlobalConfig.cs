using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZTP.Scheduler.Models
{
    public static class GlobalConfig
    {
        public static string ApplicationDirectory = Directory.GetCurrentDirectory();
        public static string PathSeparate = "\\";
        /// <summary>
        /// Ścieżka pliku z konfiguracją.
        /// </summary>
        public static string ConfigPath = $"{ApplicationDirectory}{PathSeparate}ConfigFile.xml";
        public static string CsvFile { get; set; }
        public static Smtp Smtp { get; set; } = new Smtp();
    }
}

// <copyright file="ConfigServices.cs" company="Jantar Sp. z o. o.">
// Copyright (c) Jantar Sp. z o. o.. All rights reserved.
// </copyright>

using ZTP.Scheduler.Models;

namespace ZTP.Scheduler
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Xml.Serialization;


    /// <summary>
    /// Serwis pliku konfiguracyjnego.
    /// </summary>
    public class ConfigServices
    {
        /// <summary>
        /// Zapisanie obiektu konfiguracyjnego do pliku xml.
        /// </summary>
        /// <param name="configFile">Obiekt do zapisu.</param>
        public void Save<T>(object configFile, string directoryPath)
        {
            try
            {
                XmlSerializer x = new XmlSerializer(typeof(T));

                using (TextWriter writer = new StreamWriter(directoryPath))
                {
                    x.Serialize(writer, configFile);
                }
            }
            catch (Exception ex)
            {
                //logger
                throw;
            }
        }

        /// <summary>
        /// Deserializacja obiektu z pliku xml.
        /// </summary>
        /// <returns>Obiekt pliku konfiguracyjnego.</returns>
        public T Get<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Xml file not found.");
                }

                XmlSerializer serializer = new XmlSerializer(typeof(T));
                using (Stream reader = new FileStream(filePath, FileMode.Open))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                //logger
                throw;
            }
        }
    }
}

using System.Collections;
using System.Globalization;
using System.IO;
using Akka.Actor;
using CsvHelper;

namespace SchedulerAkka.FileService
{
    public class DataWriterReceiveActor:ReceiveActor
    {
        public DataWriterReceiveActor()
        {
            Receive<RecordsWriterMessage>(Handler);
        }

        private void Handler(RecordsWriterMessage recordsWriterMessage)
        {
            if (!File.Exists(recordsWriterMessage.FilePath))
            {
                File.Create(recordsWriterMessage.FilePath);
            }
            using var streamWriter = new StreamWriter(recordsWriterMessage.FilePath);
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            csvWriter.WriteRecords(recordsWriterMessage.ObjectsToWrite);
            csvWriter.Dispose();
        }
    }
}
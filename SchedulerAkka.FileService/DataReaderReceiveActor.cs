using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Akka.Actor;
using Akka.IO;
using CsvHelper;

namespace SchedulerAkka.FileService
{
    public class DataReaderReceiveActor : ReceiveActor
    {
        public DataReaderReceiveActor()
        {
            Receive<RecordsReaderMessage>(Handler);
        }

        private void Handler(RecordsReaderMessage recordsReaderMessage)
        {
            using var streamReader = new StreamReader(recordsReaderMessage.FilePath);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            var objects = csvReader.GetRecords(recordsReaderMessage.RecordType)
                .Skip(recordsReaderMessage.Skip)
                .Take(recordsReaderMessage.Take)
                .ToList();
            Sender.Tell(objects);
        }
    }
}
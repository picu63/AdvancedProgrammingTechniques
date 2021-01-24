using System;

namespace SchedulerAkka.FileService
{
    public class RecordsReaderMessage
    {
        public RecordsReaderMessage(string filePath, Type recordType)
        {
            FilePath = filePath;
            RecordType = recordType;
        }

        public string FilePath { get; private set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public Type RecordType { get; }
    }
}
using System.Collections;
using System.Collections.Generic;

namespace SchedulerAkka.FileService
{
    public class RecordsWriterMessage
    {
        public string FilePath { get; }
        public ICollection ObjectsToWrite { get; }

        public RecordsWriterMessage(string filePath, ICollection objectsToWrite)
        {
            FilePath = filePath;
            ObjectsToWrite = objectsToWrite;
        }
    }
}
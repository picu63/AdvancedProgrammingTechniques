using System;
using System.Collections.Generic;
using System.Text;
using CQRS.MediatR.Event;

namespace Scheduler.FileService.Events
{
    public class RecordsSavedToFile : IEvent
    {
        public string FilePath { get; }
        public int NumberOfRecords { get; }
        public RecordsSavedToFile(string filePath, int numberOfRecords)
        {
            FilePath = filePath;
            NumberOfRecords = numberOfRecords;
        }
    }
}

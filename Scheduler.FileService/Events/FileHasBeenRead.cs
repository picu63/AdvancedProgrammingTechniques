using CQRS.MediatR.Event;

namespace Scheduler.FileService.Events
{
    public class FileHasBeenRead: IEvent
    {
        public FileHasBeenRead(string filePath, int numberOfRecords)
        {
            FilePath = filePath;
            NumberOfRecords = numberOfRecords;
        }

        public string FilePath { get; }
        public int NumberOfRecords { get; }
    }
}

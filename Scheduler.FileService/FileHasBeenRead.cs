using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Scheduler.FileService
{
    public class FileHasBeenRead: INotification
    {
        //public FileHasBeenRead(string filePath, int numberOfRecords)
        //{
        //    FilePath = filePath;
        //    NumberOfRecords = numberOfRecords;
        //}
        
        //public string FilePath { get; }
        //public int NumberOfRecords { get; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CQRS.MediatR.Command;

namespace Scheduler.FileService.Commands
{
    public class SaveToCsv : ICommand
    {
        
        public string FilePath { get; }
        public ICollection Collection { get; }

        public SaveToCsv(string filePath, ICollection collection)
        {
            FilePath = filePath;
            Collection = collection;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using CQRS.MediatR.Command;
using CQRS.MediatR.Query;
using CsvHelper;
using MediatR;

namespace Scheduler.FileService
{
    public class ReadFile : IQuery<ICollection>
    {
        public Type Type { get; }
        public string FilePath { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public ReadFile(Type type,string filePath)
        {
            Type = type;
            FilePath = filePath;
        }
    }
}
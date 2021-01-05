using System;
using System.Collections;
using System.Collections.Generic;
using CQRS.MediatR.Command;
using CQRS.MediatR.Query;
using CsvHelper;
using MediatR;

namespace Scheduler.FileService
{
    public class ReadFile<T> : IQuery<IEnumerable<T>>
    {
        public string FilePath { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public ReadFile(string filePath)
        {
            FilePath = filePath;
        }
    }
}
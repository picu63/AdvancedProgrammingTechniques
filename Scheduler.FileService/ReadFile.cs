using System;
using System.Collections;
using System.Collections.Generic;
using CsvHelper;
using MediatR;

namespace Scheduler.FileService
{
    public class ReadFile<T> : IRequest<ICollection<T>>
    {
        public string FilePath { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using CsvHelper;
using MediatR;

namespace Scheduler.FileService
{
    public class ReadFile<TModel> : IRequest<ICollection<TModel>>
    {
        public string FilePath { get; set; }
        public int Take { get; set; } = 1;
        public int Skip { get; set; } = 1;
    }
}

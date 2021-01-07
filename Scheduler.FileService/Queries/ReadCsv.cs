using System;
using System.Collections;
using CQRS.MediatR.Query;

namespace Scheduler.FileService.Queries
{
    public class ReadCsv : IQuery<ICollection>
    {
        public Type Type { get; }
        public string FilePath { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }

        public ReadCsv(Type type,string filePath)
        {
            Type = type;
            FilePath = filePath;
        }
    }
}
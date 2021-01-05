using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Event;
using CQRS.MediatR.Query;
using CsvHelper;
using MediatR;

namespace Scheduler.FileService
{
    public class ReadFileHandler<T> : IQueryHandler<ReadFile<T>, IEnumerable<T>>
    {
        private readonly IEventsBus _eventsBus;

        public ReadFileHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task<IEnumerable<T>> Handle(ReadFile<T> request, CancellationToken cancellationToken)
        {
            StreamReader streamReader = new StreamReader(request.FilePath);
            CsvReader csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            var records = csvReader.GetRecords<T>().Skip(request.Skip).Take(request.Take);
            return await Task.FromResult(records);
        }
    }
}
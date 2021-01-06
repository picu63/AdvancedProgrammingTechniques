using System;
using System.Collections;
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
    public class ReadFileHandler : IQueryHandler<ReadFile, ICollection>
    {
        private readonly IEventsBus _eventsBus;

        public ReadFileHandler(IEventsBus eventsBus)
        {
            _eventsBus = eventsBus;
        }

        public async Task<ICollection> Handle(ReadFile request, CancellationToken cancellationToken)
        {
            var type = request.Type;
            var streamReader = new StreamReader(request.FilePath);
            var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            reader.Configuration.HasHeaderRecord = true;
            var collection = reader.GetRecords(type).ToList();
            await _eventsBus.Publish(new FileHasBeenRead(request.FilePath, collection.Count), cancellationToken);
            return collection;
        }
    }
}
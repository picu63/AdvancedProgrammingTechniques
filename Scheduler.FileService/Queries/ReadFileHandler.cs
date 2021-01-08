using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Event;
using CQRS.MediatR.Query;
using CsvHelper;
using Scheduler.FileService.Events;

namespace Scheduler.FileService.Queries
{
    public class ReadFileHandler : IQueryHandler<ReadCsv, ICollection>
    {
        private readonly IEventBus _eventBus;

        public ReadFileHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task<ICollection> Handle(ReadCsv request, CancellationToken cancellationToken)
        {
            var type = request.Type;
            var streamReader = new StreamReader(request.FilePath);
            var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            reader.Configuration.HasHeaderRecord = true;
            var collection = reader.GetRecords(type).Skip(request.Skip).Take(request.Take).ToList();
            await _eventBus.Publish(new FileHasBeenRead(request.FilePath, collection.Count), cancellationToken);
            return collection;
        }
    }
}
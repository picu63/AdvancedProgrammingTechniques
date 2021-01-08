using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Command;
using CQRS.MediatR.Event;
using CsvHelper;
using MediatR;
using Scheduler.FileService.Events;

namespace Scheduler.FileService.Commands
{
    public class SaveHandler : ICommandHandler<SaveToCsv>
    {
        private readonly IEventBus _eventBus;

        public SaveHandler(IEventBus eventBus)
        {
            this._eventBus = eventBus;
        }
        public async Task<Unit> Handle(SaveToCsv request, CancellationToken cancellationToken)
        {
            StreamWriter streamWriter = new StreamWriter(request.FilePath);
            CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
            await csvWriter.WriteRecordsAsync(request.Collection);
            await csvWriter.DisposeAsync();
            await _eventBus.Publish(new RecordsSavedToFile(request.FilePath, request.Collection.Count), cancellationToken);
            return Unit.Value;
        }
    }
}
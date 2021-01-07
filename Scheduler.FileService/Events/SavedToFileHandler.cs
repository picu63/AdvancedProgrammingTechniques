using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Event;
using Microsoft.Extensions.Logging;

namespace Scheduler.FileService.Events
{
    public class SavedToFileHandler : IEventHandler<RecordsSavedToFile>
    {
        private readonly ILogger _logger;

        public SavedToFileHandler(ILogger<SavedToFileHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(RecordsSavedToFile notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.NumberOfRecords} records were saved to file \"{notification.FilePath}\"");
            return Task.CompletedTask;
        }
    }
}

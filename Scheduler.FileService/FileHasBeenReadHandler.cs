using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Scheduler.FileService
{
    class FileHasBeenReadHandler: INotificationHandler<FileHasBeenRead>
    {
        private readonly ILogger _logger;

        public FileHasBeenReadHandler(ILogger logger)
        {
            _logger = logger;
        }
        public Task Handle(FileHasBeenRead notification, CancellationToken cancellationToken)
        {
            //_logger.LogInformation($"{notification.NumberOfRecords} records were read from the file: \"{notification.FilePath}\" ");
            return Task.CompletedTask;
        }
    }
}

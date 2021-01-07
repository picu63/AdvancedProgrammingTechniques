using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Event;
using Microsoft.Extensions.Logging;

namespace Scheduler.FileService.Events
{
    public class FileHasBeenReadHandler: IEventHandler<FileHasBeenRead>
    {
        private readonly ILogger _logger;

        public FileHasBeenReadHandler(ILogger<FileHasBeenReadHandler> logger)
        {
            _logger = logger;
        }
        public async Task Handle(FileHasBeenRead notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{notification.NumberOfRecords} records were read from the file: \"{notification.FilePath}\" ");
            await Task.CompletedTask;
        }
    }
}

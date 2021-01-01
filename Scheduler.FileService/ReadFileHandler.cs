using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using MediatR;

namespace Scheduler.FileService
{
    public class ReadFileHandler<T> : IRequestHandler<ReadFile<T>, ICollection<T>>
    {
        private readonly IMediator _mediator;

        public ReadFileHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ICollection<T>> Handle(ReadFile<T> request, CancellationToken cancellationToken)
        {
            StreamReader streamReader = new StreamReader(request.FilePath);
            CsvReader csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            var records = csvReader.GetRecords<T>().ToList();
            await _mediator.Publish(new FileRead(), cancellationToken);
            return records;
        }
    }
}

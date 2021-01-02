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
    public class ReadFileHandler<TModel> : IRequestHandler<ReadFile<TModel>, ICollection<TModel>>
    {
        private readonly IMediator _mediator;

        public ReadFileHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ICollection<TModel>> Handle(ReadFile<TModel> request, CancellationToken cancellationToken)
        {
            var streamReader = new StreamReader(request.FilePath);
            var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
            var records = csvReader.GetRecords<TModel>().Skip(request.Skip).Take(request.Take).ToList();
            await _mediator.Publish(new FileHasBeenRead(), cancellationToken);
            return records;
        }
    }
}
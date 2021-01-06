using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CQRS.MediatR.Command;
using CQRS.MediatR.Event;
using CQRS.MediatR.Query;
using MediatR;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrdersLibrary;
using Scheduler.FileService;

namespace SchedulerAdv
{
    public class SchedulerIntervalService : IHostedService
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IQueryBus _queryBus;
        private readonly ICommandBus _commandBus;
        private readonly IEventsBus _eventsBus;
        private Timer _timer;
        public SchedulerIntervalService(ILogger<SchedulerIntervalService> logger, IQueryBus queryBus, ICommandBus commandBus, IEventsBus eventsBus)
        {
            _logger = logger;
            _queryBus = queryBus;
            _commandBus = commandBus;
            _eventsBus = eventsBus;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Orders service is starting.");
            _timer = new Timer(Run, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Orders service is stopping.");
            _timer.Dispose();
            return Task.CompletedTask;
        }

        private async void Run(object state)
        {
            var collection = await _queryBus.Send<ReadFile, ICollection>(new ReadFile(typeof(Order),
                "C:\\Users\\picu6\\source\\repos\\ZTP\\SchedulerAdv\\csv_file_200.csv")
            {Skip = 100, Take = 100});
            foreach (var order in collection)
            {
                Console.WriteLine(order);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
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
        private Timer _timer;
        public SchedulerIntervalService(ILogger<SchedulerIntervalService> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
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
            await _mediator.Send(new ReadFile<Order>{FilePath = "C:\\Users\\picu6\\source\\repos\\ZTP\\SchedulerAdv\\csv_file_10.csv" });
        }
    }
}

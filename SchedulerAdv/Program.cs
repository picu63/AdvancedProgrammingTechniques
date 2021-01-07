using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CQRS.Core.Commands;
using CQRS.Core.Events;
using CQRS.Core.Queries;
using CQRS.MediatR.Command;
using CQRS.MediatR.Event;
using CQRS.MediatR.Query;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using OrdersLibrary;
using Scheduler.FileService;
using Scheduler.FileService.Events;
using Scheduler.FileService.Queries;

namespace SchedulerAdv
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggingBuilder =>
                {
                    var configuration = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json")
                        .Build();
                    var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .CreateLogger();
                    loggingBuilder.AddSerilog(logger);
                })
                .ConfigureServices((services) =>
                {
                    services.AddHostedService<SchedulerIntervalService>()
                    .AddMediatR(
                        Assembly.GetExecutingAssembly(),
                        AppDomain.CurrentDomain.Load("Scheduler.FileService"),
                        Assembly.GetAssembly(typeof(ReadCsv)),
                        Assembly.GetAssembly(typeof(FileHasBeenRead)))
                    .AddSingleton<ICommandBus, CommandBus>()
                    .AddSingleton<IEventBus, EventBus>()
                    .AddSingleton<IQueryBus, QueryBus>(); 
                });
    }
}

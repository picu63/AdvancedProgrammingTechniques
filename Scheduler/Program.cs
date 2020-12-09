using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Scheduler
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
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<OrdersScheduler>();
                });
    }
}
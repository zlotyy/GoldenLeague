using GoldenLeague.StatisticsWorker.Services;
using GoldenLeague.StatisticsWorker.Workers;
using GoldenLeague.StatisticsWorker.Wrappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GoldenLeague.StatisticsWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var _config = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

            return Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    IConfiguration configuration = hostContext.Configuration;
                    services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

                    // workers
                    services.AddHostedService<MatchResultsWorker>();

                    // services
                    services.AddTransient<IRestServiceFactory, RestServiceFactory>();
                    services.AddTransient<IGoldenLeagueApiWrapper, GoldenLeagueApiWrapper>();
                    services.AddTransient<IFantasyApiWrapper, FantasyApiWrapper>();
                });
        }
    }
}

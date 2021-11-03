using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Queries;
using GoldenLeague.StatisticsWorker.Services;
using GoldenLeague.StatisticsWorker.Workers;
using LinqToDB.Data;
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

                    DataConnection.DefaultSettings = new LinqToDBSettings(configuration.GetConnectionString("GoldenLeagueDB"));
                    services.AddSingleton<IDbContextFactory, DbContextFactory>();

                    services.AddAutoMapper(typeof(Program));

                    // workers
                    services.AddHostedService<MatchResultsWorker>();

                    // services
                    services.AddTransient<IRestServiceFactory, RestServiceFactory>();
                    services.AddTransient<IGoldenLeagueService, GoldenLeagueService>();
                    services.AddTransient<IFantasyService, FantasyService>();

                    // commands
                    services.AddTransient<IMatchCommands, MatchCommands>();

                    // queries
                    services.AddTransient<ITeamQueries, TeamQueries>();
                });
        }
    }
}

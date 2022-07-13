using GoldenLeague.Database;
using GoldenLeague.Database.Queries;
using GoldenLeague.StatisticsWorker.Adapters;
using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Queries;
using GoldenLeague.StatisticsWorker.Services;
using GoldenLeague.StatisticsWorker.Workers;
using LinqToDB.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using System;

namespace GoldenLeague.StatisticsWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = LogManager.GetCurrentClassLogger();
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                // NLog: catch any exception and log it.
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                LogManager.Shutdown();
            }
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

                    services.AddLogging(loggingBuilder =>
                    {
                        // configure Logging with NLog
                        loggingBuilder.ClearProviders();
                        loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                        loggingBuilder.AddNLog(configuration);
                    });

                    services.AddAutoMapper(typeof(Program));

                    // DATETIME TODO !!!
                    //services.
                        //.AddJsonOptions(options =>
                        // {
                        //     options.JsonSerializerOptions.Converters.Insert(0, new DateTimeJsonConverter());
                        // });

                    // workers
                    //services.AddHostedService<MatchResultsWorker>();
                    //services.AddHostedService<MatchRecordsWorker>();
                    services.AddHostedService<PerDayWorker>();

                    // services
                    services.AddTransient<IRestServiceFactory, RestServiceFactory>();
                    services.AddTransient<IFantasyService, FantasyService_USUNAC>();
                    services.AddTransient<IFootballApiService, FootballApiService>();

                    // commands
                    services.AddTransient<ITeamCommands, TeamCommands>();
                    services.AddTransient<IMatchCommands, MatchCommands>();
                    services.AddTransient<ITeamStatisticsCommands, TeamStatisticsCommands>();
                    services.AddTransient<ICompetitionsCommands, CompetitionsCommands>();

                    // queries
                    services.AddTransient<IBaseQueries, BaseQueries>();
                    services.AddTransient<ITeamQueries, TeamQueries>();
                    services.AddTransient<ICompetitionsQueries, CompetitionsQueries>();

                    // adapter - jeœli zmieni siê serwis do pobierania danych, wystarczy zmieniæ tutaj adapter na inny
                    services.AddTransient<IFootballDataAdapter, FootballApiAdapter>();
                });
        }
    }
}

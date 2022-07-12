using GoldenLeague.StatisticsWorker.Adapters;
using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Workers
{
    public class PerDayWorker : BackgroundService
    {
        private readonly ILogger<PerDayWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFootballApiService _footballApiService;
        private readonly IFootballApiAdapter _footballApiAdapter;   // TODO Adapter Factory
        private readonly ICompetitionsCommands _competitionsCommands;
        private const int _DELAY_MULTIPLIER = 1000 * 60 * 60 * 24;

        public PerDayWorker(ILogger<PerDayWorker> logger, IOptions<AppSettings> config, 
            IFootballApiService footballApiService, IFootballApiAdapter footballApiAdapter,
            ICompetitionsCommands competitionsCommands)
        {
            _logger = logger;
            _config = config.Value;
            _footballApiService = footballApiService;
            _footballApiAdapter = footballApiAdapter; // TODO Adapter Factory
            _competitionsCommands = competitionsCommands;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SetLeaguesData();
                await Task.Delay(_DELAY_MULTIPLIER, stoppingToken);
            }
        }

        private async Task SetLeaguesData()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation($"START {nameof(SetLeaguesData)}, {DateTimeOffset.Now}");

                await Task.Run(() =>
                {
                    var currentLeagues = _footballApiService.GetCurrentLeagues();
                    var mappedData = _footballApiAdapter.MapToCompetitions(currentLeagues);
                    _competitionsCommands.UpsertCompetitions(mappedData);
                });

                _logger.LogInformation($"FINISHED {nameof(SetLeaguesData)}, timeElapsed: {stopwatch.Elapsed} ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(SetLeaguesData)}");
            }
            finally
            {
                stopwatch.Reset();
            }
        }
    }
}

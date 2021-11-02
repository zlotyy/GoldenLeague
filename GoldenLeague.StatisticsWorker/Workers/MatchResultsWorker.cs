using GoldenLeague.StatisticsWorker.Services;
using GoldenLeague.StatisticsWorker.Wrappers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Workers
{
    public class MatchResultsWorker : BackgroundService
    {
        private readonly ILogger<MatchResultsWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFantasyApiWrapper _fantasyApiWrapper;
        private readonly IGoldenLeagueApiWrapper _goldenLeagueApiWrapper;

        private const int _DELAY_MULTIPLIER = 1000 * 60;

        public MatchResultsWorker(ILogger<MatchResultsWorker> logger, IOptions<AppSettings> config,
            IFantasyApiWrapper fantasyApiWrapper, IGoldenLeagueApiWrapper goldenLeagueApiWrapper)
        {
            _logger = logger;
            _config = config.Value;
            _fantasyApiWrapper = fantasyApiWrapper;
            _goldenLeagueApiWrapper = goldenLeagueApiWrapper;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await SynchronizeMatchesForCurrentSeason();
            while (!stoppingToken.IsCancellationRequested)
            {
                await SynchronizeMatchResults();
                await Task.Delay(_config.MatchResultWorkerDelay * _DELAY_MULTIPLIER, stoppingToken);
            }
        }

        private async Task SynchronizeMatchesForCurrentSeason()
        {
            _logger.LogInformation($"START {nameof(SynchronizeMatchesForCurrentSeason)}, {DateTimeOffset.Now}");

            await Task.Run(() =>
            {
                var allMatches = _fantasyApiWrapper.GetFixtures();
                // upsert matches (insert not existing and update existing finished)
            });
        }

        private async Task SynchronizeMatchResults()
        {
            _logger.LogInformation($"START {nameof(SynchronizeMatchResults)}, {DateTimeOffset.Now}");
            
            await Task.Run(() =>
            {
                var matches = _fantasyApiWrapper.GetFixtures(10);
            });
        }
    }
}

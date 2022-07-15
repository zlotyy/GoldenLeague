using GoldenLeague.StatisticsWorker.Adapters;
using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Queries;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Workers
{
    public class FrequentWorker : BackgroundService
    {
        private readonly ILogger<InfrequentWorker> _logger;
        private readonly IFootballDataAdapter _footballDataAdapter;
        private readonly ICompetitionsQueries _competitionsQueries;
        private readonly ITeamQueries _teamQueries;
        private readonly IMatchQueries _matchQueries;
        private readonly IMatchCommands _matchCommands;
        private readonly AppSettings _config;
        private const int _DELAY_MULTIPLIER = 1000 * 60 * 15;

        public FrequentWorker(ILogger<InfrequentWorker> logger, IOptions<AppSettings> config, 
            IFootballDataAdapter footballDataAdapter, ICompetitionsQueries competitionsQueries,
            ITeamQueries teamQueries, IMatchCommands matchCommands, IMatchQueries matchQueries)
        {
            _logger = logger;
            _config = config.Value;
            _footballDataAdapter = footballDataAdapter;
            _competitionsQueries = competitionsQueries;
            _teamQueries = teamQueries;
            _matchCommands = matchCommands;
            _matchQueries = matchQueries;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SetLiveMatchesData();
                await Task.Delay(_DELAY_MULTIPLIER, stoppingToken);
            }
        }

        private async Task SetLiveMatchesData()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation($"START {nameof(SetLiveMatchesData)}, {DateTimeOffset.Now}");

                await Task.Run(() =>
                {
                    var fixtures = _footballDataAdapter.GetFixturesLive();
                    var matches = _footballDataAdapter.MapToMatches(fixtures);
                    
                    _matchCommands.UpsertMatches(matches);
                });

                _logger.LogInformation($"FINISHED {nameof(SetLiveMatchesData)}, timeElapsed: {stopwatch.Elapsed} ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(SetLiveMatchesData)}");
            }
            finally
            {
                stopwatch.Reset();
            }
        }
    }
}

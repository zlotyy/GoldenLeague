using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Adapters;
using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Queries;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Workers
{
    public class PerDayWorker : BackgroundService
    {
        private readonly ILogger<PerDayWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFootballDataAdapter _footballDataAdapter;
        private readonly ICompetitionsCommands _competitionsCommands;
        private readonly ICompetitionsQueries _competitionsQueries;
        private readonly ITeamCommands _teamCommands;
        private const int _DELAY_MULTIPLIER = 1000 * 60 * 60 * 24;

        public PerDayWorker(ILogger<PerDayWorker> logger, IOptions<AppSettings> config, 
            IFootballDataAdapter footballDataAdapter, ICompetitionsCommands competitionsCommands,
            ICompetitionsQueries competitionsQueries, ITeamCommands teamCommands)
        {
            _logger = logger;
            _config = config.Value;
            _footballDataAdapter = footballDataAdapter;
            _competitionsCommands = competitionsCommands;
            _competitionsQueries = competitionsQueries;
            _teamCommands = teamCommands;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SetLeaguesData();
                await SetTeamsData();
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
                    var currentLeagues = _footballDataAdapter.GetCurrentLeagues();
                    var mappedData = _footballDataAdapter.MapToCompetitions(currentLeagues);
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

        private async Task SetTeamsData()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation($"START {nameof(SetTeamsData)}, {DateTimeOffset.Now}");

                await Task.Run(() =>
                {
                    var mappedTeams = new List<Teams>();
                    var activeCompetitions = _competitionsQueries.GetActiveCompetitions().ToList();
                    activeCompetitions.ForEach(competitions =>
                    {
                        var teams = _footballDataAdapter.GetTeams(competitions);
                        mappedTeams.AddRange(_footballDataAdapter.MapToTeams(teams, competitions));
                    });
                    _teamCommands.UpsertTeams(mappedTeams);
                });

                _logger.LogInformation($"FINISHED {nameof(SetTeamsData)}, timeElapsed: {stopwatch.Elapsed} ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(SetTeamsData)}");
            }
            finally
            {
                stopwatch.Reset();
            }
        }
    }
}

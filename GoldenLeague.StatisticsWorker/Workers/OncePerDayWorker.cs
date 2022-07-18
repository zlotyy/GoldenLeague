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
    public class OncePerDayWorker : BackgroundService
    {
        private readonly ILogger<OncePerDayWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFootballDataAdapter _footballDataAdapter;
        private readonly ICompetitionsCommands _competitionsCommands;
        private readonly ICompetitionsQueries _competitionsQueries;
        private readonly ITeamCommands _teamCommands;
        private readonly ITeamQueries _teamQueries;
        private readonly IMatchCommands _matchCommands;
        private const int DELAY_MULTIPLIER = 1000 * 60 * 60 * 24;

        public OncePerDayWorker(ILogger<OncePerDayWorker> logger, IOptions<AppSettings> config, 
            IFootballDataAdapter footballDataAdapter, ICompetitionsCommands competitionsCommands,
            ICompetitionsQueries competitionsQueries, ITeamCommands teamCommands,
            ITeamQueries teamQueries, IMatchCommands matchCommands)
        {
            _logger = logger;
            _config = config.Value;
            _footballDataAdapter = footballDataAdapter;
            _competitionsCommands = competitionsCommands;
            _competitionsQueries = competitionsQueries;
            _teamCommands = teamCommands;
            _teamQueries = teamQueries;
            _matchCommands = matchCommands;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SetLeaguesData();
                await SetTeamsData();
                await SetMatchesData();
                await Task.Delay(DELAY_MULTIPLIER, stoppingToken);
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

        private async Task SetMatchesData()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation($"START {nameof(SetMatchesData)}, {DateTimeOffset.Now}");

                await Task.Run(() =>
                {
                    var fixtures = _footballDataAdapter.GetFixturesIncoming();
                    var matches = _footballDataAdapter.MapToMatches(fixtures);
                    
                    _matchCommands.UpsertMatches(matches);
                });

                _logger.LogInformation($"FINISHED {nameof(SetMatchesData)}, timeElapsed: {stopwatch.Elapsed} ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(SetMatchesData)}");
            }
            finally
            {
                stopwatch.Reset();
            }
        }
    }
}

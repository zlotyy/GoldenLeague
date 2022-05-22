using AutoMapper;
using GoldenLeague.Database.Queries;
using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Workers
{
    public class MatchResultsWorker : BackgroundService
    {
        private readonly ILogger<MatchResultsWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFantasyService _fantasyApiWrapper;
        private readonly IMapper _mapper;
        private readonly IMatchCommands _matchCommands;
        private readonly ITeamStatisticsCommands _teamStatisticsCommands;
        private readonly IBaseQueries _baseQueries;

        private const int _DELAY_MULTIPLIER = 1000 * 60;
        private readonly int _currentSeasonNo;

        public MatchResultsWorker(ILogger<MatchResultsWorker> logger, IOptions<AppSettings> config,
            IFantasyService fantasyApiWrapper, IMapper mapper, IMatchCommands matchCommands, IBaseQueries baseQueries,
            ITeamStatisticsCommands teamStatisticsCommands)
        {
            _logger = logger;
            _config = config.Value;
            _fantasyApiWrapper = fantasyApiWrapper;
            _mapper = mapper;
            _matchCommands = matchCommands;
            _baseQueries = baseQueries;
            _teamStatisticsCommands = teamStatisticsCommands;

            _currentSeasonNo = _baseQueries.GetCurrentSeasonNo();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {            
            while (!stoppingToken.IsCancellationRequested)
            {
                await SynchronizeMatchResults();
                await Task.Delay(_config.MatchResultWorkerDelay * _DELAY_MULTIPLIER, stoppingToken);
            }
        }

        private async Task SynchronizeMatchResults()
        {
            _logger.LogInformation($"START {nameof(SynchronizeMatchResults)}, {DateTimeOffset.Now}");
            
            await Task.Run(() =>
            {
                var currentGameweekNo = _baseQueries.GetCurrentGameweekNo();

                for (int gameweekNo=1; gameweekNo<=currentGameweekNo; gameweekNo++)
                {
                    var matches = _fantasyApiWrapper.GetMatches(gameweekNo);
                    if (matches != null)
                    {
                        _matchCommands.UpsertMatchesData(matches);
                    }
                }

                _teamStatisticsCommands.UpdateTeamsStatistics();
            });
        }
    }
}

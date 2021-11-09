using AutoMapper;
using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Services;
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
        private readonly IFantasyService _fantasyApiWrapper;
        private readonly IMapper _mapper;
        private readonly IMatchCommands _commands;

        private const int _DELAY_MULTIPLIER = 1000 * 60;
        private readonly int _currentSeasonNo;

        public MatchResultsWorker(ILogger<MatchResultsWorker> logger, IOptions<AppSettings> config,
            IFantasyService fantasyApiWrapper, IMapper mapper, IMatchCommands commands)
        {
            _logger = logger;
            _config = config.Value;
            _fantasyApiWrapper = fantasyApiWrapper;
            _mapper = mapper;
            _commands = commands;

            _currentSeasonNo = 2022; // IGoldenLeagueService.GetCurrentSeasonNo
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
                var currentGameweekNo = 10;

                var matches = _fantasyApiWrapper.GetMatches(currentGameweekNo);
                _commands.UpsertMatches(matches);
            });
        }
    }
}

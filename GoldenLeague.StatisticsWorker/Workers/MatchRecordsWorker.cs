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
    public class MatchRecordsWorker : BackgroundService
    {
        private readonly ILogger<MatchRecordsWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFantasyService _fantasyApiWrapper;
        private readonly IMapper _mapper;
        private readonly IMatchCommands _commands;

        private const int _DELAY_MULTIPLIER = 1000 * 60 * 60 * 24;
        private readonly int _currentSeasonNo;

        public MatchRecordsWorker(ILogger<MatchRecordsWorker> logger, IOptions<AppSettings> config,
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
                await InsertMatchesForCurrentSeason();
                await Task.Delay(_DELAY_MULTIPLIER, stoppingToken);
            }
        }

        private async Task InsertMatchesForCurrentSeason()
        {
            _logger.LogInformation($"START {nameof(InsertMatchesForCurrentSeason)}, {DateTimeOffset.Now}");

            await Task.Run(() =>
            {
                var allMatches = _fantasyApiWrapper.GetMatches();
                _commands.InsertNewMatches(allMatches);
            });
        }
    }
}

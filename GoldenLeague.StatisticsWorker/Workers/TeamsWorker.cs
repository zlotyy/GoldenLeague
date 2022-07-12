using GoldenLeague.StatisticsWorker.Commands;
using GoldenLeague.StatisticsWorker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Workers
{
    public class TeamsWorker : BackgroundService
    {

        private readonly ILogger<TeamsWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFootballApiService _footballApi;
        private readonly ITeamCommands _commands;

        private const int _DELAY_MULTIPLIER = 1000 * 60 * 60 * 24;

        public TeamsWorker(ILogger<TeamsWorker> logger, IOptions<AppSettings> config,
            IFootballApiService footballApi, ITeamCommands commands)
        {
            _logger = logger;
            _config = config.Value;
            _footballApi = footballApi;
            _commands = commands;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await InsertTeamsForCompetitions();
                await Task.Delay(_DELAY_MULTIPLIER, stoppingToken);
            }
        }

        private async Task InsertTeamsForCompetitions()
        {
            _logger.LogInformation($"START {nameof(InsertTeamsForCompetitions)}, {DateTimeOffset.Now}");

            await Task.Run(() =>
            {
                //var allMatches = _footballApi.GetTeams();
                //_commands.UpdateTeams(allMatches);
            });
        }
    }
}

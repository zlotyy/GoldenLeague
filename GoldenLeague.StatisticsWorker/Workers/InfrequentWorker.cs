using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Adapters;
using GoldenLeague.StatisticsWorker.Queries;
using GoldenLeague.StatisticsWorker.Services;
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
    public class InfrequentWorker : BackgroundService
    {
        private readonly ILogger<InfrequentWorker> _logger;
        private readonly IFootballDataAdapter _footballDataAdapter;
        private readonly ICompetitionsQueries _competitionsQueries;
        private readonly AppSettings _config;
        private const int _DELAY_MULTIPLIER = 1000 * 60 * 60;

        public InfrequentWorker(ILogger<InfrequentWorker> logger, IOptions<AppSettings> config, 
            IFootballDataAdapter footballDataAdapter, ICompetitionsQueries competitionsQueries)
        {
            _logger = logger;
            _config = config.Value;
            _footballDataAdapter = footballDataAdapter;
            _competitionsQueries = competitionsQueries;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_DELAY_MULTIPLIER, stoppingToken);
            }
        }
    }
}

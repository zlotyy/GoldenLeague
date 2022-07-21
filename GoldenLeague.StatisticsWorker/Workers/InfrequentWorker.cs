using GoldenLeague.StatisticsWorker.Adapters;
using GoldenLeague.StatisticsWorker.Queries;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly int _workerDelay = 1000 * 60 * 60;

        public InfrequentWorker(ILogger<InfrequentWorker> logger, IOptions<AppSettings> config, 
            IFootballDataAdapter footballDataAdapter, ICompetitionsQueries competitionsQueries)
        {
            _logger = logger;
            _config = config.Value;
            _footballDataAdapter = footballDataAdapter;
            _competitionsQueries = competitionsQueries;

            _workerDelay = 1000 * 60 * _config.InFrequentWorkerDelay;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_workerDelay, stoppingToken);
            }
        }
    }
}

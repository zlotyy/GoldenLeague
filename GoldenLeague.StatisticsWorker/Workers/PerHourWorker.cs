using GoldenLeague.StatisticsWorker.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Workers
{
    public class PerHourWorker : BackgroundService
    {
        private readonly ILogger<PerHourWorker> _logger;
        private readonly AppSettings _config;
        private readonly IFootballApiService _footballApiService;
        private const int _DELAY_MULTIPLIER = 1000 * 60 * 60;

        public PerHourWorker(ILogger<PerHourWorker> logger, IOptions<AppSettings> config, IFootballApiService footballApiService)
        {
            _logger = logger;
            _config = config.Value;
            _footballApiService = footballApiService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await SetFixturesData();
                await Task.Delay(_DELAY_MULTIPLIER, stoppingToken);
            }
        }

        private async Task SetFixturesData()
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                _logger.LogInformation($"START {nameof(SetFixturesData)}, {DateTimeOffset.Now}");

                await Task.Run(() =>
                {
                    var fixtures = _footballApiService.GetFixtures();
                });

                _logger.LogInformation($"FINISHED {nameof(SetFixturesData)}, timeElapsed: {stopwatch.Elapsed} ms");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(SetFixturesData)}");
            }
            finally
            {
                stopwatch.Reset();
            }
        }
    }
}

using GoldenLeague.StatisticsWorker.Models.Fantasy;
using GoldenLeague.StatisticsWorker.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using GoldenLeague.Common.Extensions;
using System;

namespace GoldenLeague.StatisticsWorker.Wrappers
{
    public interface IFantasyApiWrapper
    {
        List<FixtureMatchModel> GetFixtures(int gameweekNo);
        List<FixtureMatchModel> GetFixtures();
    }

    public class FantasyApiWrapper : IFantasyApiWrapper
    {
        private readonly ILogger<FantasyApiWrapper> _logger;
        private readonly AppSettings _config;
        private readonly RestService _restService;

        public FantasyApiWrapper(ILogger<FantasyApiWrapper> logger, IOptions<AppSettings> config, IRestServiceFactory restServiceFactory)
        {
            _logger = logger;
            _config = config.Value;
            _restService = restServiceFactory.CreateFantasyService();
        }

        public List<FixtureMatchModel> GetFixtures(int gameweekNo)
        {
            var result = new List<FixtureMatchModel>();
            try
            {
                var response = _restService.Get<List<FixtureMatchModel>>(FantasyApiEndpoints.Fixtures(gameweekNo));
                if (response.IsSuccessful)
                {
                    result = response.Data;
                    _logger.LogTrace($"SUCCESS {nameof(GetFixtures)} for gameweek {gameweekNo}, data: {result.ToJson(pretify: true)}");
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetFixtures)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetFixtures)}");
            }
            return result;
        }

        public List<FixtureMatchModel> GetFixtures()
        {
            var result = new List<FixtureMatchModel>();
            try
            {
                var response = _restService.Get<List<FixtureMatchModel>>(FantasyApiEndpoints.FixturesBase);
                if (response.IsSuccessful)
                {
                    result = response.Data;
                    _logger.LogTrace($"SUCCESS {nameof(GetFixtures)}, data: {result.ToJson(pretify: true)}");
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetFixtures)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetFixtures)}");
            }
            return result;
        }
    }

    public static class FantasyApiEndpoints
    {
        public static string FixturesBase => $"fixtures";
        public static string Fixtures(int gameweekNo) => $"{FixturesBase}/?event={gameweekNo}";
    }
}

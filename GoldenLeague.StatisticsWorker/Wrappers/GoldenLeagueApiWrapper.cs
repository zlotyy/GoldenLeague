using GoldenLeague.StatisticsWorker.Services;
using GoldenLeague.TransportModels.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using GoldenLeague.Common.Extensions;

namespace GoldenLeague.StatisticsWorker.Wrappers
{
    public interface IGoldenLeagueApiWrapper
    {
        List<MatchModel> GetCurrentSeasonMatches();
    }

    public class GoldenLeagueApiWrapper : IGoldenLeagueApiWrapper
    {
        private readonly ILogger<GoldenLeagueApiWrapper> _logger;
        private readonly AppSettings _config;
        private readonly RestService _restService;

        public GoldenLeagueApiWrapper(ILogger<GoldenLeagueApiWrapper> logger, IOptions<AppSettings> config, IRestServiceFactory restServiceFactory)
        {
            _logger = logger;
            _config = config.Value;
            _restService = restServiceFactory.CreateGoldenLeagueService();
        }

        public List<MatchModel> GetCurrentSeasonMatches()
        {
            var result = new List<MatchModel>();
            try
            {
                var response = _restService.Get<List<MatchModel>>(GoldenLeagueApiEndpoints.MatchesCurrentSeason);
                if (response.IsSuccessful)
                {
                    result = response.Data;
                    _logger.LogTrace($"SUCCESS {nameof(GetCurrentSeasonMatches)}, data: {result.ToJson(pretify: true)}");
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetCurrentSeasonMatches)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCurrentSeasonMatches)}");
            }
            return result;
        }
    }

    public static class GoldenLeagueApiEndpoints
    {
        public static string MatchesBase => $"matches";
        public static string MatchesCurrentSeason => $"{MatchesBase}/current-season";
    }
}

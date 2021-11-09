using GoldenLeague.TransportModels.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using GoldenLeague.Common.Extensions;

namespace GoldenLeague.StatisticsWorker.Services
{
    public interface IGoldenLeagueService
    {
        //List<TeamModel> GetTeams();
    }

    public class GoldenLeagueService : IGoldenLeagueService
    {
        private readonly ILogger<GoldenLeagueService> _logger;
        private readonly AppSettings _config;
        private readonly RestService _restService;

        public GoldenLeagueService(ILogger<GoldenLeagueService> logger, IOptions<AppSettings> config, IRestServiceFactory restServiceFactory)
        {
            _logger = logger;
            _config = config.Value;
            _restService = restServiceFactory.CreateGoldenLeagueService();
        }

        //public List<TeamModel> GetTeams()
        //{
        //    var result = new List<TeamModel>();
        //    try
        //    {
        //        var response = _restService.Get<List<TeamModel>>(GoldenLeagueApiEndpoints.TeamsBase);
        //        if (response.IsSuccessful)
        //        {
        //            result = response.Data;
        //            _logger.LogTrace($"SUCCESS {nameof(GetTeams)}, data: {result.ToJson(pretify: true)}");
        //        }
        //        else
        //        {
        //            _logger.LogError($"Error during {nameof(GetTeams)}");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error during {nameof(GetTeams)}");
        //    }
        //    return result;
        //}
    }

    public static class GoldenLeagueApiEndpoints
    {
        public static string MatchesBase => "matches";
        public static string MatchesCurrentSeason => $"{MatchesBase}/current-season";

        public static string TeamsBase => "teams";
    }
}

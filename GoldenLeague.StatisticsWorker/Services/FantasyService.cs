using GoldenLeague.StatisticsWorker.Models.Fantasy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using GoldenLeague.Common.Extensions;
using System;
using AutoMapper;
using GoldenLeague.TransportModels.Common;
using System.Linq;
using GoldenLeague.StatisticsWorker.Queries;
using GoldenLeague.Database;

namespace GoldenLeague.StatisticsWorker.Services
{
    public interface IFantasyService
    {
        List<MatchModel> GetMatches(int gameweekNo);
        List<MatchModel> GetMatches();
    }

    public class FantasyService : IFantasyService
    {
        private readonly ILogger<FantasyService> _logger;
        private readonly AppSettings _config;
        private readonly RestService _fantasyService;
        private readonly IGoldenLeagueService _goldenLeagueService;
        private readonly IMapper _mapper;
        private readonly ITeamQueries _teamQueries;

        private readonly int _currentSeasonNo;
        private Dictionary<int, Guid> _teamsDictionary;

        public FantasyService(ILogger<FantasyService> logger, IOptions<AppSettings> config, IRestServiceFactory restServiceFactory,
            IMapper mapper, IGoldenLeagueService goldenLeagueService, ITeamQueries teamQueries)
        {
            _logger = logger;
            _config = config.Value;
            _fantasyService = restServiceFactory.CreateFantasyService();
            _goldenLeagueService = goldenLeagueService;
            _mapper = mapper;
            _teamQueries = teamQueries;

            _currentSeasonNo = 2022; // _goldenLeagueService.GetC
            _teamsDictionary = GetTeams().ToDictionary(s => s.ForeignKey.Value, s => s.TeamId);
        }

        public List<MatchModel> GetMatches(int gameweekNo)
        {
            var result = new List<MatchModel>();
            try
            {
                var response = _fantasyService.Get<List<FixtureMatchModel>>(FantasyApiEndpoints.Fixtures(gameweekNo));
                if (response.IsSuccessful)
                {
                    _logger.LogTrace($"SUCCESS {nameof(GetMatches)} for gameweek {gameweekNo}, data: {result.ToJson(pretify: true)}");
                    result = _mapper.Map<List<MatchModel>>(response.Data, opt => opt.Items.Add("SeasonNo", _currentSeasonNo));
                    result.ForEach(match =>
                    {
                        match.HomeTeamId = _teamsDictionary.GetValueOrDefault(match.HomeTeamFK.Value);
                        match.AwayTeamId = _teamsDictionary.GetValueOrDefault(match.AwayTeamFK.Value);
                    });
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetMatches)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetMatches)}");
            }
            return result;
        }

        public List<MatchModel> GetMatches()
        {
            var result = new List<MatchModel>();
            try
            {
                var response = _fantasyService.Get<List<FixtureMatchModel>>(FantasyApiEndpoints.FixturesBase);
                if (response.IsSuccessful)
                {
                    _logger.LogTrace($"SUCCESS {nameof(GetMatches)}, data: {result.ToJson(pretify: true)}");
                    result = _mapper.Map<List<MatchModel>>(response.Data, opt => opt.Items.Add("SeasonNo", _currentSeasonNo));
                    result.ForEach(match =>
                    {
                        match.HomeTeamId = _teamsDictionary.GetValueOrDefault(match.HomeTeamFK.Value);
                        match.AwayTeamId = _teamsDictionary.GetValueOrDefault(match.AwayTeamFK.Value);
                    });
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetMatches)}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetMatches)}");
            }
            return result;
        }

        private List<Teams> GetTeams()
        {
            var tmp = _teamQueries.GetTeams().Where(x => x.ForeignKey.HasValue).ToList();
            return tmp;
        }
    }

    public static class FantasyApiEndpoints
    {
        public static string FixturesBase => $"fixtures";
        public static string Fixtures(int gameweekNo) => $"{FixturesBase}/?event={gameweekNo}";
    }
}

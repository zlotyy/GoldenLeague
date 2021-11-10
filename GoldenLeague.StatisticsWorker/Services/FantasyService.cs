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
using GoldenLeague.Database.Queries;

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
        private readonly IBaseQueries _baseQueries;

        private readonly int _currentSeasonNo;
        private Dictionary<int, Guid> _teamsDictionary;

        public FantasyService(ILogger<FantasyService> logger, IOptions<AppSettings> config, IRestServiceFactory restServiceFactory,
            IMapper mapper, IGoldenLeagueService goldenLeagueService, ITeamQueries teamQueries, IBaseQueries baseQueries)
        {
            _logger = logger;
            _config = config.Value;
            _fantasyService = restServiceFactory.CreateFantasyService();
            _goldenLeagueService = goldenLeagueService;
            _mapper = mapper;
            _teamQueries = teamQueries;
            _baseQueries = baseQueries;

            _currentSeasonNo = _baseQueries.GetCurrentSeasonNo();
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
                    _logger.LogTrace($"SUCCESS {nameof(GetMatches)} for gameweek {gameweekNo}, data: {response.Data.ToJson()}");
                    result = MapToMatchModel(response.Data);
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
                    _logger.LogTrace($"SUCCESS {nameof(GetMatches)}, data: {response.Data.ToJson()}");
                    result = MapToMatchModel(response.Data);
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

        private List<MatchModel> MapToMatchModel(List<FixtureMatchModel> data)
        {
            var result = _mapper.Map<List<MatchModel>>(data, opt => opt.Items.Add("SeasonNo", _currentSeasonNo));
            result.ForEach(match =>
            {
                match.HomeTeamId = _teamsDictionary.GetValueOrDefault(match.HomeTeamFK.Value);
                match.AwayTeamId = _teamsDictionary.GetValueOrDefault(match.AwayTeamFK.Value);
            });

            return result;
        }

        private List<Teams> GetTeams()
        {
            return _teamQueries.GetTeams().Where(x => x.ForeignKey.HasValue).ToList();
        }
    }

    public static class FantasyApiEndpoints
    {
        public static string FixturesBase => $"fixtures";
        public static string Fixtures(int gameweekNo) => $"{FixturesBase}/?event={gameweekNo}";
    }
}

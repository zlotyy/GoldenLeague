using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Base;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Fixtures;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Teams;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Leagues;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using GoldenLeague.Common.Extensions;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Services
{
    public interface IFootballApiService
    {
        IEnumerable<LeagueResponse> GetCurrentLeagues();
        IEnumerable<TeamResponse> GetTeams(int leagueId, int season);
        IEnumerable<FixtureResponse> GetFixtures(int leagueId, int season);
        IEnumerable<FixtureResponse> GetFutureFixtures(int leagueId, int season);
        IEnumerable<FixtureResponse> GetLiveFixtures(IEnumerable<int> fixtureIds);
    }

    public class FootballApiService : IFootballApiService
    {
        private readonly ILogger<FootballApiService> _logger;
        private readonly AppSettings _config;
        private readonly RestService _restService;

        public FootballApiService(ILogger<FootballApiService> logger, IOptions<AppSettings> config, IRestServiceFactory restServiceFactory)
        {
            _logger = logger;
            _config = config.Value;
            _restService = restServiceFactory.CreateFootballApiService();
        }

        public IEnumerable<LeagueResponse> GetCurrentLeagues()
        {
            var result = new List<LeagueResponse>();
            _logger.LogTrace($"START {nameof(GetCurrentLeagues)}");

            try
            {
                var response = _restService.Get<ArrayResponseModel<LeagueResponse>>(FootballApiEndpoints.LeaguesCurrent);
                if (response.IsSuccessful)
                {
                    _logger.LogTrace($"SUCCESS {nameof(GetCurrentLeagues)}, data: {response.Data.ToJson()}");
                    result = response.Data.Response.ToList();
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetCurrentLeagues)} | {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCurrentLeagues)}");
            }

            return result;
        }

        public IEnumerable<TeamResponse> GetTeams(int leagueId, int season)
        {
            var result = new List<TeamResponse>();
            _logger.LogTrace($"START {nameof(GetTeams)}");

            try
            {
                var response = _restService.Get<ArrayResponseModel<TeamResponse>>(FootballApiEndpoints.TeamsCurrent(leagueId, season));
                if (response.IsSuccessful)
                {
                    _logger.LogTrace($"SUCCESS {nameof(GetTeams)}, data: {response.Data.ToJson()}");
                    result = response.Data.Response.ToList();
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetTeams)} | {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetTeams)}");
            }

            return result;
        }

        public IEnumerable<FixtureResponse> GetFixtures(int leagueId, int season)
        {
            var result = new List<FixtureResponse>();
            _logger.LogTrace($"START {nameof(GetFixtures)} | leagueId: {leagueId}, season: {season}");

            try
            {
                var now = DateTime.Now;

                var response = _restService.Get<ArrayResponseModel<FixtureResponse>>(FootballApiEndpoints.FixturesForLeague(leagueId, season));
                if (response.IsSuccessful)
                {
                    _logger.LogTrace($"SUCCESS {nameof(GetFixtures)}, data: {response.Data.ToJson()}");
                    result = response.Data.Response.ToList();
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetFixtures)} | {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetFixtures)}");
            }

            return result;
        }

        public IEnumerable<FixtureResponse> GetFutureFixtures(int leagueId, int season)
        {
            var result = new List<FixtureResponse>();
            _logger.LogTrace($"START {nameof(GetFutureFixtures)}");

            try
            {
                var now = DateTime.Now;

                var response = _restService.Get<ArrayResponseModel<FixtureResponse>>(
                    FootballApiEndpoints.FixturesIncoming(leagueId, season, now.ToString("yyyy-MM-dd"), now.AddYears(1).ToString("yyyy-MM-dd")));
                if (response.IsSuccessful)
                {
                    _logger.LogTrace($"SUCCESS {nameof(GetFutureFixtures)}, data: {response.Data.ToJson()}");
                    result = response.Data.Response.ToList();
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetFutureFixtures)} | {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetFutureFixtures)}");
            }

            return result;
        }

        public IEnumerable<FixtureResponse> GetLiveFixtures(IEnumerable<int> fixtureIds)
        {
            var result = new List<FixtureResponse>();
            _logger.LogTrace($"START {nameof(GetLiveFixtures)}");

            try
            {
                var now = DateTime.Now;
                var response = _restService
                    .Get<ArrayResponseModel<FixtureResponse>>(FootballApiEndpoints.FixturesLive(fixtureIds));
                if (response.IsSuccessful)
                {
                    _logger.LogTrace($"SUCCESS {nameof(GetLiveFixtures)}, data: {response.Data.ToJson()}");
                    result = response.Data.Response.ToList();
                }
                else
                {
                    _logger.LogError($"Error during {nameof(GetLiveFixtures)} | {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetLiveFixtures)}");
            }

            return result;
        }
    }

    public static class FootballApiEndpoints
    {
        public static string Leagues => "leagues";
        public static string LeaguesCurrent => $"{Leagues}?current=true";
        public static string Teams => "teams";
        public static string TeamsCurrent(int leagueId, int season) => $"{Teams}?league={leagueId}&season={season}";
        public static string Fixtures => "fixtures";
        public static string FixturesForLeague(int leagueId, int season) => $"{Fixtures}?league={leagueId}&season={season}";
        public static string FixturesIncoming(int leagueId, int season, string from, string to) => $"{Fixtures}?league={leagueId}&season={season}&from={from}&to={to}";
        public static string FixturesLive(IEnumerable<int> fixtureIds) => $"{Fixtures}?ids={string.Join("-", fixtureIds)}";
    }
}

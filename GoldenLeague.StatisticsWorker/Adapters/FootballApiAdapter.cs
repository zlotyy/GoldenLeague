using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Leagues;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Teams;
using GoldenLeague.StatisticsWorker.Models.Interfaces;
using GoldenLeague.StatisticsWorker.Queries;
using GoldenLeague.StatisticsWorker.Services;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Adapters
{
    public class FootballApiAdapter : IFootballDataAdapter
    {
        private readonly IFootballApiService _service;
        private readonly ICompetitionsQueries _competitionsQueries;

        public FootballApiAdapter(IFootballApiService service, ICompetitionsQueries competitionsQueries)
        {
            _service = service;
            _competitionsQueries = competitionsQueries;
        }

        public IEnumerable<ILeagueResponse> GetCurrentLeagues()
        {
            return _service.GetCurrentLeagues();
        }

        public IEnumerable<ITeamResponse> GetTeams(Competitions competitions)
        {
            var result = new List<TeamResponse>();
            
            var leagueId = int.Parse(competitions.ForeignKey);
            var season = competitions.CurrentSeasonNo;
            var leagueTeams = _service.GetTeams(leagueId, season);
            result.AddRange(leagueTeams);

            return result;
        }

        public IEnumerable<IFixtureResponse> GetFixtures()
        {
            return _service.GetFixtures();
        }

        public IEnumerable<Competitions> MapToCompetitions<T>(IEnumerable<T> leagues)
        {
            var competitions = (leagues as IEnumerable<LeagueResponse>).Select(x => new Competitions
            {
                ForeignKey = x.League.Id.ToString(),
                CompetitionsName = x.League.Name,
                CurrentSeasonNo = x.Seasons.FirstOrDefault(s => s.Current).Year,
                CountryName = x.Country.Name,
                CountryCode = x.Country.Code
            });

            return competitions;
        }

        public IEnumerable<Teams> MapToTeams<T>(IEnumerable<T> teams, Competitions competitions)
        {
            var mappedTeams = (teams as IEnumerable<TeamResponse>).Select(x => new Teams
            {
                ForeignKey = x.Team.Id.ToString(),
                TeamName = x.Team.Name,
                TeamNameAbbreviation = x.Team.Code,
                CompetitionId = competitions.CompetitionsId
            });

            return mappedTeams;
        }
    }
}

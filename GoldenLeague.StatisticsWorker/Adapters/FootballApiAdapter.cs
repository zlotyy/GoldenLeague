using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Helpers;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Fixtures;
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
        private readonly ITeamQueries _teamQueries;
        private readonly IMatchQueries _matchQueries;

        public FootballApiAdapter(IFootballApiService service, ICompetitionsQueries competitionsQueries,
            ITeamQueries teamQueries, IMatchQueries matchQueries)
        {
            _service = service;
            _competitionsQueries = competitionsQueries;
            _teamQueries = teamQueries;
            _matchQueries = matchQueries;
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

        public IEnumerable<IFixtureResponse> GetAllFixtures()
        {
            var result = new List<FixtureResponse>();

            // pobierz aktywne ligi
            var activeCompetitions = _competitionsQueries.GetActiveCompetitions().ToList();

            // dla każdej ligi pobierz wszystkie mecze w sezonie
            activeCompetitions.ForEach(competitions =>
            {
                var leagueId = int.Parse(competitions.ForeignKey);
                var season = competitions.CurrentSeasonNo;
                var leagueFixtures = _service.GetFixtures(leagueId, season);
                result.AddRange(leagueFixtures);
            });

            return result;
        }

        public IEnumerable<IFixtureResponse> GetFixturesIncoming()
        {
            var result = new List<FixtureResponse>();

            // pobierz aktywne ligi
            var activeCompetitions = _competitionsQueries.GetActiveCompetitions().ToList();

            // dla każdej ligi pobierz mecze, które się nie odbyły
            activeCompetitions.ForEach(competitions =>
            {
                var leagueId = int.Parse(competitions.ForeignKey);
                var season = competitions.CurrentSeasonNo;
                var leagueFixtures = _service.GetFutureFixtures(leagueId, season);
                result.AddRange(leagueFixtures);
            });

            return result;
        }

        public IEnumerable<IFixtureResponse> GetFixturesLive()
        {
            var result = new List<FixtureResponse>();

            // pobierz trwające mecze
            var liveMatchesIds = _matchQueries.GetLiveMatchesForeignKeys().Select(x => int.Parse(x));

            for (int i=0; i<liveMatchesIds.Count(); i+=10)
            {
                var limitedIds = liveMatchesIds.Skip(i).Take(10);
                if (limitedIds.Any())
                {
                    result.AddRange(_service.GetLiveFixtures(limitedIds));
                }
            }

            return result;
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

        public IEnumerable<Matches> MapToMatches<T>(IEnumerable<T> fixtures)
        {
            // pobierz mapowanie foreignKey ligi na id
            var competitionsKeys = _competitionsQueries.GetCompetitionsKeys().ToDictionary(x => int.Parse(x.Key), x => x.Value);

            // pobierz mapowanie foreignKey zespołu na id
            var teamKeys = _teamQueries.GetTeamKeys().ToDictionary(x => int.Parse(x.Key), x => x.Value);

            var matches = (fixtures as IEnumerable<FixtureResponse>).Select(x => new Matches
            {
                ForeignKey = x.Fixture.Id.ToString(),
                CompetitionId = competitionsKeys.GetValueOrDefault(x.League.Id),
                SeasonNo = x.League.Season,
                GameweekNo = FootballApiHelpers.ParseRound(x.League.Round),
                HomeTeamId = teamKeys.GetValueOrDefault(x.Teams.Home.Id),
                AwayTeamId = teamKeys.GetValueOrDefault(x.Teams.Away.Id),
                MatchDateTime = x.Fixture.Date,
                HomeTeamScore = x.Goals.Home,
                AwayTeamScore = x.Goals.Away,
                IsFinished = FootballApiHelpers.IsMatchFinished(x.Fixture.Status.Short)
            });

            return matches;
        }
    }
}

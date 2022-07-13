using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Models.Interfaces;
using System.Collections.Generic;

namespace GoldenLeague.StatisticsWorker.Adapters
{
    public interface IFootballDataAdapter
    {
        IEnumerable<ILeagueResponse> GetCurrentLeagues();
        IEnumerable<ITeamResponse> GetTeams(Competitions competitions);
        IEnumerable<IFixtureResponse> GetFixtures();
        IEnumerable<Competitions> MapToCompetitions<T>(IEnumerable<T> leagues);
        IEnumerable<Teams> MapToTeams<T>(IEnumerable<T> teams, Competitions competitions);
        //IEnumerable<Matches> MapToMatches<T>(IEnumerable<T> fixtures);
    }
}

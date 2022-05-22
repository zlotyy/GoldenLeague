using GoldenLeague.Database;
using LinqToDB;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface ITeamQueries
    {
        List<Teams> GetTeams();
        List<PremierLeagueTable> GetTeamsStandings();
    }

    public class TeamQueries : ITeamQueries
    {
        private readonly IDbContextFactory _dbContextFactory;

        public TeamQueries(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<Teams> GetTeams()
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Teams.ToList();
            }
        }

        public List<PremierLeagueTable> GetTeamsStandings()
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.PremierLeagueTable
                    .LoadWith(x => x.Team)
                    .OrderByDescending(x => x.Points)
                    .ThenByDescending(x => x.GoalsScored - x.GoalsConceded)
                    .ThenByDescending(x => x.GoalsScored)
                    .ThenBy(x => x.MatchesPlayed)
                    .ToList();
            }
        }
    }
}

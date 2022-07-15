using GoldenLeague.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Queries
{
    public interface ITeamQueries
    {
        List<Teams> GetTeams();
        Dictionary<string, Guid> GetTeamKeys();
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

        public Dictionary<string, Guid> GetTeamKeys()
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Teams.ToDictionary(x => x.ForeignKey, x => x.TeamId);
            }
        }
    }
}

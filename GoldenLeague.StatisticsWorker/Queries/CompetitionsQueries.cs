using GoldenLeague.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Queries
{
    public interface ICompetitionsQueries
    {
        IEnumerable<Competitions> GetActiveCompetitions();
        Dictionary<string, Guid> GetCompetitionsKeys();
    }

    public class CompetitionsQueries : ICompetitionsQueries
    {
        private readonly IDbContextFactory _dbContextFactory;

        public CompetitionsQueries(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IEnumerable<Competitions> GetActiveCompetitions()
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Competitions.Where(x => x.IsActive).ToList();
            }
        }

        public Dictionary<string, Guid> GetCompetitionsKeys()
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.Competitions.ToDictionary(x => x.ForeignKey, x => x.CompetitionsId);
            }
        }
    }
}

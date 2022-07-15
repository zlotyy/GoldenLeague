using GoldenLeague.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Queries
{
    public interface IMatchQueries
    {
        IEnumerable<string> GetLiveMatchesForeignKeys();
    }

    public class MatchQueries : IMatchQueries
    {
        private readonly IDbContextFactory _dbContextFactory;

        public MatchQueries(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public IEnumerable<string> GetLiveMatchesForeignKeys()
        {
            var now = DateTime.Now;
            DateTime periodStart = now.AddHours(-3);
            DateTime periodEnd = now.AddHours(3);

            using (var db = _dbContextFactory.Create())
            {
                return db.Matches
                    .Where(x => !x.IsFinished
                        && x.MatchDateTime > periodStart
                        && x.MatchDateTime < periodEnd)
                    .Select(x => x.ForeignKey)
                    .ToList();
            }
        }
    }
}

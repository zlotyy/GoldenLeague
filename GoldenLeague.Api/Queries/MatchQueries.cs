using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.Database.Queries;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface IMatchQueries : IBaseQueries
    {
        IEnumerable<VMatch> GetMatchesFull(int seasonNo, int gameweekNo);
        IEnumerable<VMatch> GetMatchesFull(int seasonNo);
        IEnumerable<VMatch> GetCurrentSeasonMatchesFull();
        IEnumerable<VMatch> GetCurrentGameweekMatchesFull();
    }

    public class MatchQueries : BaseQueries, IMatchQueries
    {
        private readonly ILogger<MatchQueries> _logger;
        private readonly IMapper _mapper;

        public MatchQueries(IDbContextFactory dbContextFactory, ILogger<MatchQueries> logger, IMapper mapper) : base(dbContextFactory)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<VMatch> GetMatchesFull(int seasonNo, int gameweekNo)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo && x.GameweekNo == gameweekNo).ToList();
            }
        }

        public IEnumerable<VMatch> GetMatchesFull(int seasonNo)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo).OrderBy(o => o.MatchDateTime).ToList();
            }
        }

        public IEnumerable<VMatch> GetCurrentSeasonMatchesFull()
        {
            var seasonNo = GetCurrentSeasonNo();

            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo).OrderBy(o => o.MatchDateTime).ToList();
            }
        }

        public IEnumerable<VMatch> GetCurrentGameweekMatchesFull()
        {
            var seasonNo = GetCurrentSeasonNo();
            var gameweekNo = GetCurrentGameweekNo();

            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo && x.GameweekNo == gameweekNo).ToList();
            }
        }
    }
}

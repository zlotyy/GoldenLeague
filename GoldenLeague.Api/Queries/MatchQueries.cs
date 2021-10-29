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
        IEnumerable<VMatch> GetMatches(int seasonNo, int gameweekNo);
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

        public IEnumerable<VMatch> GetMatches(int seasonNo, int gameweekNo)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo && x.GameweekNo == gameweekNo).ToList();
            }
        }
    }
}

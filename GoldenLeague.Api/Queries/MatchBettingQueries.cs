using AutoMapper;
using GoldenLeague.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Api.Queries
{
    public interface IMatchBettingQueries
    {
        IEnumerable<VMatchBetting> GetMatchBetting(Guid userId, int seasonNo);
    }

    public class MatchBettingQueries : IMatchBettingQueries
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<MatchBettingQueries> _logger;
        private readonly IMapper _mapper;

        public MatchBettingQueries(IDbContextFactory dbContextFactory, ILogger<MatchBettingQueries> logger, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<VMatchBetting> GetMatchBetting(Guid userId, int seasonNo)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.GetUserMatchBetting(userId, seasonNo);
            }
        }
    }
}

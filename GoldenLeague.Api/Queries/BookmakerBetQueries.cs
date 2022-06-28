using AutoMapper;
using GoldenLeague.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface IBookmakerBetQueries
    {
        IEnumerable<VBookmakerBet> GetUserBets(Guid userId);
    }

    public class BookmakerBetQueries : IBookmakerBetQueries
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<BookmakerBetQueries> _logger;
        private readonly IMapper _mapper;

        public BookmakerBetQueries(IDbContextFactory dbContextFactory, ILogger<BookmakerBetQueries> logger, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<VBookmakerBet> GetUserBets(Guid userId)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.GetUserBookmakerBets(userId).ToList();
            }
        }
    }
}

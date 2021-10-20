using AutoMapper;
using GoldenLeague.Database;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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
            // TODO Ten widok musi zwracać wszystkie typy wszystkich użytkowników
            // Procedura powinna pobierać wszystkie mecze użytkownika, również nieobstawione
            using (var db = _dbContextFactory.Create())
            {
                var query = db.VMatchBetting
                    .Where(x => x.UserId == userId && x.SeasonNo == seasonNo)
                    .OrderBy(x => x.MatchDateTime);

                return query.ToList();
            }
        }
    }
}

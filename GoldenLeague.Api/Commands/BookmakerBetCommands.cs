using AutoMapper;
using GoldenLeague.Api.Queries;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.MatchBetting;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Commands
{
    public interface IBookmakerBetCommands
    {
        bool UpdateBet(List<BookmakerBetModel> matchBetting);
    }

    public class BookmakerBetCommands : IBookmakerBetCommands
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IBookmakerBetQueries _queries;
        private readonly ILogger<BookmakerBetCommands> _logger;
        private readonly IMapper _mapper;

        public BookmakerBetCommands(IDbContextFactory dbContextFactory, IBookmakerBetQueries queries,
            ILogger<BookmakerBetCommands> logger, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _queries = queries;
            _logger = logger;
            _mapper = mapper;
        }

        public bool UpdateBet(List<BookmakerBetModel> bets)
        {
            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    bets.ForEach(mb =>
                    {
                        if (mb.MatchResultBet.HomeTeamScoreBet.HasValue == mb.MatchResultBet.AwayTeamScoreBet.HasValue)
                        {
                            db.BookmakerBets
                                .Where(x => x.MatchId == mb.Match.MatchId && x.UserId == mb.UserId)
                                .Set(x => x.HomeTeamScore, mb.MatchResultBet.HomeTeamScoreBet)
                                .Set(x => x.AwayTeamScore, mb.MatchResultBet.AwayTeamScoreBet)
                                .Update();
                        }
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpdateBet)}");
                return false;
            }
        }
    }
}

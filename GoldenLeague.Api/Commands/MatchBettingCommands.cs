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
    public interface IMatchBettingCommands
    {
        bool UpdateMatchBetting(List<MatchBettingModel> matchBetting);
    }

    public class MatchBettingCommands : IMatchBettingCommands
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMatchBettingQueries _queries;
        private readonly ILogger<MatchBettingCommands> _logger;
        private readonly IMapper _mapper;

        public MatchBettingCommands(IDbContextFactory dbContextFactory, IMatchBettingQueries queries,
            ILogger<MatchBettingCommands> logger, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _queries = queries;
            _logger = logger;
            _mapper = mapper;
        }

        public bool UpdateMatchBetting(List<MatchBettingModel> matchBetting)
        {
            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    matchBetting.ForEach(mb =>
                    {
                        if (mb.MatchResult.HomeTeam.TeamScoreBet.HasValue 
                            && mb.MatchResult.AwayTeam.TeamScoreBet.HasValue)
                        {
                            db.MatchBetting
                                .Where(x => x.MatchId == mb.MatchId && x.UserId == mb.UserId)
                                .Set(x => x.HomeTeamScore, mb.MatchResult.HomeTeam.TeamScoreBet)
                                .Set(x => x.AwayTeamScore, mb.MatchResult.AwayTeam.TeamScoreBet)
                                .Update();
                        }
                    });
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpdateMatchBetting)}");
                return false;
            }
        }
    }
}

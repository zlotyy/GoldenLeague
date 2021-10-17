using GoldenLeague.Database;
using GoldenLeague.TransportModels.MatchBetting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface IMatchBettingQueries
    {
        IEnumerable<MatchBettingModel> GetMatchBetting(Guid userId, int seasonNo);
    }

    public class MatchBettingQueries : IMatchBettingQueries
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<MatchBettingQueries> _logger;

        public MatchBettingQueries(IDbContextFactory dbContextFactory, ILogger<MatchBettingQueries> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public IEnumerable<MatchBettingModel> GetMatchBetting(Guid userId, int seasonNo)
        {
            var result = new List<MatchBettingModel>();

            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var query = db.VMatchBetting
                        .Where(x => x.UserId == userId && x.SeasonNo == seasonNo)
                        .OrderBy(x => x.MatchDateTime);
                    //  .ProjectTo // TODO Automapper

                    result = query
                        .Select(
                            s => new MatchBettingModel(
                                userId, s.MatchId, seasonNo, s.GameweekNo, s.MatchDateTime, new MatchResultModel(
                                    new TeamMatchDetailsModel(s.HomeTeamId, s.HomeTeamName, s.HomeTeamScoreBet, s.HomeTeamScoreActual),
                                    new TeamMatchDetailsModel(s.AwayTeamId, s.AwayTeamName, s.AwayTeamScoreBet, s.AwayTeamScoreActual)
                                )
                            )
                        )
                        .ToList();
                }
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetMatchBetting)}");
            }

            return result;
        }
    }
}

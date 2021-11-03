using GoldenLeague.TransportModels.Common;
using System;

namespace GoldenLeague.TransportModels.MatchBetting
{
    public class MatchBettingModel
    {
        public MatchBettingModel()
        {

        }

        public MatchBettingModel(Guid userId, MatchFullModel match, MatchResultBetModel matchResultBet)
        {
            UserId = userId;
            Match = match;
            MatchResultBet = matchResultBet;
        }

        public Guid UserId { get; set; }
        public MatchFullModel Match { get; set; }
        public MatchResultBetModel MatchResultBet { get; set; }
    }

    public class MatchResultBetModel
    {
        public MatchResultBetModel()
        {

        }

        public MatchResultBetModel(int? homeTeamScoreBet, int? awayTeamScoreBet, int? bettingPoints, BettingResultEnum? bettingResult)
        {
            HomeTeamScoreBet = homeTeamScoreBet;
            AwayTeamScoreBet = awayTeamScoreBet;
            BettingPoints = bettingPoints;
            BettingResult = bettingResult;
        }

        public int? HomeTeamScoreBet { get; set; }
        public int? AwayTeamScoreBet { get; set; }
        public int? BettingPoints { get; set; }
        public BettingResultEnum? BettingResult { get; set; }
    }
}

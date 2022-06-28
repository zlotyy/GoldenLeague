using GoldenLeague.TransportModels.Common;

namespace GoldenLeague.TransportModels.Bookmaker
{
    public class BookmakerBetRecord
    {
        public BookmakerBetRecord()
        {

        }

        public BookmakerBetRecord(MatchFullModel match, MatchResultBetModel matchResultBet)
        {
            Match = match;
            MatchResultBet = matchResultBet;
        }

        public MatchFullModel Match { get; set; }
        public MatchResultBetModel MatchResultBet { get; set; }
    }

    public class MatchResultBetModel
    {
        public MatchResultBetModel()
        {

        }

        public MatchResultBetModel(int? homeTeamScoreBet, int? awayTeamScoreBet, int? bettingPoints)
        {
            HomeTeamScoreBet = homeTeamScoreBet;
            AwayTeamScoreBet = awayTeamScoreBet;
            BettingPoints = bettingPoints;
        }

        public int? HomeTeamScoreBet { get; set; }
        public int? AwayTeamScoreBet { get; set; }
        public int? BettingPoints { get; set; }
        public BookmakerBetResultEnum? BettingResult
            => BettingPoints == 0 ? BookmakerBetResultEnum.MISSED
                : BettingPoints == 1 ? BookmakerBetResultEnum.PARTIALLY_HIT
                : BettingPoints == 3 ? BookmakerBetResultEnum.HIT
                : default(BookmakerBetResultEnum?);
    }
}

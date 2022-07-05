using System;

namespace GoldenLeague.TransportModels.Bookmaker
{
    public class LeagueRankRecordModel
    {
        public Guid UserId { get; set; }
        public string UserLogin { get; set; }
        public int UserRanking { get; set; }
        public int UserPoints { get; set; }
        public int UserCorrectBets { get; set; }
        public int UserCorrectResults { get; set; }
    }
}

using System;

namespace GoldenLeague.TransportModels.Common
{
    public class MatchModel
    {
        public Guid MatchId { get; set; }
        public int ForeignKey { get; set; }
        public int SeasonNo { get; set; }
        public int GameweekNo { get; set; }
        public DateTime MatchDateTime { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public int? HomeTeamFK { get; set; }
        public int? AwayTeamFK { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}

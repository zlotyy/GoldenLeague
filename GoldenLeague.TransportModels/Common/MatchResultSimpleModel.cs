using System;

namespace GoldenLeague.TransportModels.Common
{
    public class MatchResultSimpleModel
    {
        public Guid MatchId { get; set; }
        public DateTime MatchDateTime { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}

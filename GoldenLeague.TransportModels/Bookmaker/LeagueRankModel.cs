using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.TransportModels.Bookmaker
{
    public class LeagueRankModel
    {
        public Guid LeagueId { get; set; }
        public string LeagueName { get; set; }
        public int SeasonNo { get; set; }
        public IEnumerable<LeagueRankRecordModel> Users { get; set; } = Enumerable.Empty<LeagueRankRecordModel>();
    }
}

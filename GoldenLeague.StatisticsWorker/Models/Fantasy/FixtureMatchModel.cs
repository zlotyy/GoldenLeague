using System;

namespace GoldenLeague.StatisticsWorker.Models.Fantasy
{
    public class FixtureMatchModel
    {
        public int Event { get; set; }
        public DateTime Kickoff_Time { get; set; }
        public int Team_H { get; set; }
        public int? Team_H_Score { get; set; }
        public int Team_A { get; set; }
        public int? Team_A_Score { get; set; }
    }
}

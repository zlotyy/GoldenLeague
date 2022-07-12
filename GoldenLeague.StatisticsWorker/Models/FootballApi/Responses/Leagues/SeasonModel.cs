using System;

namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Leagues
{
    public class SeasonModel
    {
        public int Year { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool Current { get; set; }
    }
}

using System;

namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Fixtures
{
    public class FixtureModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public FixtureStatus Status { get; set; }
    }
}

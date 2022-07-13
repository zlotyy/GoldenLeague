using GoldenLeague.StatisticsWorker.Models.Interfaces;

namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Fixtures
{
    public class FixtureResponse : IFixtureResponse
    {
        public FixtureModel Fixture { get; set; }
        public FixtureLeagueModel League { get; set; }
        public FixtureTeamsModel Teams { get; set; }
        public FixtureGoalsModel Goals { get; set; }
    }
}

namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Fixtures
{
    public class FixtureResponse
    {
        public FixtureModel Fixture { get; set; }
        public FixtureLeagueModel League { get; set; }
        public FixtureTeamsModel Teams { get; set; }
        public FixtureGoalsModel Goals { get; set; }
    }
}

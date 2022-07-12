namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Fixtures
{
    public class FixtureTeamsModel
    {
        public TeamModel Home { get; set; }
        public TeamModel Away { get; set; }
    }

    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

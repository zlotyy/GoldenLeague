using GoldenLeague.StatisticsWorker.Models.Interfaces;

namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Teams
{
    public class TeamResponse : ITeamResponse
    {
        public TeamModel Team { get; set; }
    }
}

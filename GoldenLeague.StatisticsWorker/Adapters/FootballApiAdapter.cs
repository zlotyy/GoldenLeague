using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Leagues;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Adapters
{
    public interface IFootballApiAdapter
    {
        IEnumerable<Competitions> MapToCompetitions(IEnumerable<LeagueResponse> leagues);
    }

    public class FootballApiAdapter : IFootballApiAdapter
    {
        public IEnumerable<Competitions> MapToCompetitions(IEnumerable<LeagueResponse> leagues)
        {
            var competitions = leagues.Select(x => new Competitions
            {
                ForeignKey = x.League.Id.ToString(),
                CompetitionsName = x.League.Name,
                CurrentSeasonNo = x.Seasons.FirstOrDefault(s => s.Current).Year
            }).ToList();

            return competitions;
        }
    }
}

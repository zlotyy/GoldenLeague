using GoldenLeague.StatisticsWorker.Models.Interfaces;
using System.Collections.Generic;

namespace GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Leagues
{
    public class LeagueResponse : ILeagueResponse
    {
        public LeagueModel League { get; set; }
        public CountryModel Country { get; set; }
        public IEnumerable<SeasonModel> Seasons { get; set; }
    }
}

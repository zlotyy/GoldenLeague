using System;

namespace GoldenLeague.TransportModels.Common
{
    public class CompetitionModel
    {
        public Guid CompetitionId { get; set; }
        public string CompetitionName { get; set; }
        public string CompetitionIcon { get; set; }
        public string CountryIcon { get; set; }
        public int CurrentSeasonNo { get; set; }
        public int CurrentGameweekNo { get; set; }
    }
}

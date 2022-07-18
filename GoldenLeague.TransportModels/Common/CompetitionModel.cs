using System;

namespace GoldenLeague.TransportModels.Common
{
    public class CompetitionModel
    {
        public Guid CompetitionsId { get; set; }
        public string CompetitionsName { get; set; }
        public string CompetitionsIcon { get; set; }
        public string CountryIcon { get; set; }
        public int CurrentSeasonNo { get; set; }
    }
}

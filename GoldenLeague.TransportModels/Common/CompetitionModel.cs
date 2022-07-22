using System;

namespace GoldenLeague.TransportModels.Common
{
    public class CompetitionModel
    {
        public Guid CompetitionsId { get; set; }
        public string CompetitionsName { get; set; }
        public string CompetitionsIcon { get; set; }
        public int CurrentSeasonNo { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string CountryIcon { get; set; }
        public string CompetitionsFullName => $"{CompetitionsName} ({CountryName})";
    }
}

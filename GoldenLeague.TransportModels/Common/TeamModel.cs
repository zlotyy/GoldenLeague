using System;

namespace GoldenLeague.TransportModels.Common
{
    public class TeamModel
    {
        public TeamModel()
        {

        }

        public TeamModel(Guid teamId, string teamName, string teamNameShort, string teamNameAbbreviation)
        {
            TeamId = teamId;
            TeamName = teamName;
            TeamNameShort = teamNameShort;
            TeamNameAbbreviation = teamNameAbbreviation;
        }

        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamNameShort { get; set; }
        public string TeamNameAbbreviation { get; set; }
    }
}

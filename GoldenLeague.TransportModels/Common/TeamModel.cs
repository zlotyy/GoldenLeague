using System;

namespace GoldenLeague.TransportModels.Common
{
    public class TeamModel
    {
        public TeamModel()
        {

        }

        public TeamModel(string foreignKey)
        {
            ForeignKey = foreignKey;
        }

        public TeamModel(Guid teamId, string foreignKey, string teamName, string teamNameShort, string teamNameAbbreviation)
        {
            TeamId = teamId;
            ForeignKey = foreignKey;
            TeamName = teamName;
            TeamNameShort = teamNameShort;
            TeamNameAbbreviation = teamNameAbbreviation;
        }

        public Guid TeamId { get; set; }
        public string ForeignKey { get; set; }
        public string TeamName { get; set; }
        public string TeamNameShort { get; set; }
        public string TeamNameAbbreviation { get; set; }
    }
}

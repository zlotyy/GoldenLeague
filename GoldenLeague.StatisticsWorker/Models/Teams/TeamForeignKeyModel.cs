using System;

namespace GoldenLeague.StatisticsWorker.Models.Teams
{
    public class TeamForeignKeyModel
    {
        public Guid TeamId { get; set; }
        public string ForeignKey { get; set; }
    }
}

using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Users;
using System;
using System.Collections.Generic;

namespace GoldenLeague.TransportModels.Bookmaker
{
    public class LeagueModel
    {
        public Guid LeagueId { get; set; }
        public string LeagueName { get; set; }
        public bool IsPrivate { get; set; }
        public Guid? InsertUserId { get; set; }
        public DateTime InsertDate { get; set; }
        public IEnumerable<CompetitionModel> Competitions { get; set; }
        public IEnumerable<UserModel> Participants { get; set; }
    }
}

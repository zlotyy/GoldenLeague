using GoldenLeague.TransportModels.Common;
using System;
using System.Collections.Generic;

namespace GoldenLeague.TransportModels.Bookmaker
{
    public class EntryLeagueModel
    {
        public Guid LeagueId { get; set; }
        public string LeagueName { get; set; }
        public bool IsPrivate { get; set; }
        public Guid? InsertUserId { get; set; }
        public string InsertUserLogin { get; set; }
        public DateTime InsertDate { get; set; }
        public IEnumerable<CompetitionModel> Competitions { get; set; }
        /// <summary>
        /// Miejsce w tabeli użytkownika, który pobiera dane dla ligi
        /// </summary>
        public int? EntryRank { get; set; }
    }
}

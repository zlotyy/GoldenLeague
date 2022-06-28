using System;
using System.Collections.Generic;

namespace GoldenLeague.TransportModels.Bookmaker
{
    public class BookmakerUserBetsModel
    {
        public BookmakerUserBetsModel()
        {
            UserBets = new List<BookmakerBetRecord>();
        }

        public Guid CompetitionsId { get; set; }
        public IEnumerable<BookmakerBetRecord> UserBets { get; set; }
    }
}

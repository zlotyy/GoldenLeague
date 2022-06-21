﻿using System;
using System.Collections.Generic;

namespace GoldenLeague.TransportModels.Bookmaker
{
    public class LeagueCreateModel
    {
        public string Name { get; set; }
        public IEnumerable<Guid> Competitions { get; set; }
        public Guid InsertUserId { get; set; }
    }
}
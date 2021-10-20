﻿using System.ComponentModel;

namespace GoldenLeague.TransportModels.MatchBetting
{
    public enum BettingResultEnum
    {
        /// <summary>
        /// Nietrafiony
        /// </summary>
        [Description("Nietrafiony")]
        MISSED = 1,
        /// <summary>
        /// Częściowo trafiony
        /// </summary>
        [Description("Częściowo trafiony")]
        PARTIALLY_HIT = 2,
        /// <summary>
        /// Trafiony
        /// </summary>
        [Description("Trafiony")]
        HIT = 3
    }
}

﻿using System;

namespace GoldenLeague.Helpers
{
    public static class ApiUrlHelper
    {
        public static string UsersBase => "users";
        public static string UsersAuthenticate => $"{UsersBase}/authenticate";
        public static string UserBookmakerBetsGet(Guid userId, int seasonNo) => $"{UsersBase}/{userId}/bookmaker-bets/?seasonNo={seasonNo}";
        public static string UserBookmakerBetsUpdate(Guid userId) => $"{UsersBase}/{userId}/bookmaker-bets";

        public static string MatchesBase => "matches";
        public static string MatchesCurrentGameweek => $"{MatchesBase}/current-gameweek";

        public static string TeamsBase => "teams";
        public static string TeamsRanking => $"{TeamsBase}/ranking";
    }
}

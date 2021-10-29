using System;

namespace GoldenLeague.Helpers
{
    public static class ApiUrlHelper
    {
        public static string UsersBase => "users";
        public static string UserMatchBettingGet(Guid userId, int seasonNo) => $"{UsersBase}/{userId}/match-betting/?seasonNo={seasonNo}";
        public static string UserMatchBettingUpdate(Guid userId) => $"{UsersBase}/{userId}/match-betting";

        public static string MatchesBase => "matches";
        public static string MatchesCurrentGameweek => $"{MatchesBase}/current-gameweek";
    }
}

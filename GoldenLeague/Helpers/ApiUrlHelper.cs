using System;

namespace GoldenLeague.Helpers
{
    public static class ApiUrlHelper
    {
        public static string BaseUser => "users";
        public static string UserMatchBettingGet(Guid userId, int seasonNo) => $"{BaseUser}/{userId}/match-betting/?seasonNo={seasonNo}";
        public static string UserMatchBettingUpdate(Guid userId) => $"{BaseUser}/{userId}/match-betting";
    }
}

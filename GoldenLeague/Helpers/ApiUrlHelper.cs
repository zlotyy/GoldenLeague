using System;

namespace GoldenLeague.Helpers
{
    public static class ApiUrlHelper
    {
        public static string CommonBase => "common";
        public static string Competitions => $"{CommonBase}/competitions";

        public static string UsersBase => "users";
        public static string UserAuthenticate => $"{UsersBase}/authenticate";
        public static string UserPasswordChange(Guid userId) => $"{UsersBase}/{userId}/password-change";
        public static string UserExists(Guid userId) => $"{UsersBase}/{userId}/exists";
        public static string UserBookmakerBetsGet(Guid userId) => $"{UsersBase}/{userId}/bookmaker-bets";
        public static string UserBookmakerBetsUpdate(Guid userId) => $"{UsersBase}/{userId}/bookmaker-bets";
        public static string UserBookmakerLeaguesJoined(Guid userId) => $"{UsersBase}/{userId}/bookmaker-leagues-joined";
        public static string UserBookmakerLeagueJoin(Guid userId) => $"{UsersBase}/{userId}/bookmaker-league-join";
        public static string UserBookmakerLeagueLeave(Guid userId) => $"{UsersBase}/{userId}/bookmaker-league-leave";
        public static string UserBookmakerCompetitions(Guid userId) => $"{UsersBase}/{userId}/bookmaker-competitions";
        public static string UserBookmakerIncomingMatches(Guid userId) => $"{UsersBase}/{userId}/bookmaker-incoming-matches";

        public static string MatchesBase => "matches";
        public static string MatchesCurrentGameweek => $"{MatchesBase}/current-gameweek";

        public static string TeamsBase => "teams";
        public static string TeamsRanking => $"{TeamsBase}/ranking";

        public static string BookmakerLeaguesBase => "bookmaker-leagues";
        public static string BookmakerLeagueRank(Guid leagueId) => $"{BookmakerLeaguesBase}/{leagueId}/rank";
    }
}

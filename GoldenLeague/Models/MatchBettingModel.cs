using System;

namespace GoldenLeague.Models
{
    public class MatchBettingModel
    {
        public MatchBettingModel(Guid playerId, Guid matchId, DateTime matchDateTime, TeamBetDetails homeTeamBetDetails, TeamBetDetails awayTeamBetDetails)
        {
            PlayerId = playerId;
            MatchId = matchId;
            MatchDateTime = matchDateTime;
            HomeTeamBetDetails = homeTeamBetDetails;
            AwayTeamBetDetails = awayTeamBetDetails;
        }

        public Guid PlayerId { get; set; }
        public Guid MatchId { get; set; }
        public DateTime MatchDateTime { get; set; }
        public TeamBetDetails HomeTeamBetDetails { get; set; }
        public TeamBetDetails AwayTeamBetDetails { get; set; }
    }

    public class TeamBetDetails
    {
        public TeamBetDetails(Guid teamId, string teamName, int? teamGoalsBet, int? teamGoalsActual)
        {
            TeamId = teamId;
            TeamName = teamName;
            TeamGoalsBet = teamGoalsBet;
            TeamGoalsActual = teamGoalsActual;
        }

        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public int? TeamGoalsBet { get; set; }
        public int? TeamGoalsActual { get; set; }
    }
}

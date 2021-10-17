using System;

namespace GoldenLeague.TransportModels.MatchBetting
{
    public class MatchBettingModel
    {
        public MatchBettingModel()
        {

        }

        public MatchBettingModel(Guid userId, Guid matchId, int seasonNo, int gameweekNo, DateTime matchDateTime, MatchResultModel matchResult)
        {
            UserId = userId;
            MatchId = matchId;
            SeasonNo = seasonNo;
            GameweekNo = gameweekNo;
            MatchDateTime = matchDateTime;
            MatchResult = matchResult;
        }

        public Guid UserId { get; set; }
        public Guid MatchId { get; set; }
        public int SeasonNo { get; set; }
        public int GameweekNo { get; set; }
        public DateTime MatchDateTime { get; set; }
        public MatchResultModel MatchResult { get; set; }
    }

    public class MatchResultModel
    {
        public MatchResultModel()
        {

        }

        public MatchResultModel(TeamMatchDetailsModel homeTeam, TeamMatchDetailsModel awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
        }

        public TeamMatchDetailsModel HomeTeam { get; set; }
        public TeamMatchDetailsModel AwayTeam { get; set; }
    }

    public class TeamMatchDetailsModel
    {
        public TeamMatchDetailsModel()
        {

        }

        public TeamMatchDetailsModel(Guid teamId, string teamName, int? teamScoreBet, int? teamScoreActual)
        {
            TeamId = teamId;
            TeamName = teamName;
            TeamScoreBet = teamScoreBet;
            TeamScoreActual = teamScoreActual;
        }

        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public int? TeamScoreBet { get; set; }
        public int? TeamScoreActual { get; set; }
    }
}

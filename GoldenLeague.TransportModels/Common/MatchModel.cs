﻿using System;

namespace GoldenLeague.TransportModels.Common
{
    public class MatchModel
    {
        public MatchModel()
        {

        }

        public MatchModel(Guid matchId, int seasonNo, int gameweekNo, DateTime matchDateTime, TeamModel homeTeam, TeamModel awayTeam, int? homeTeamScore, int? awayTeamScore)
        {
            MatchId = matchId;
            SeasonNo = seasonNo;
            GameweekNo = gameweekNo;
            MatchDateTime = matchDateTime;
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeTeamScore = homeTeamScore;
            AwayTeamScore = awayTeamScore;
        }

        public Guid MatchId { get; set; }
        public int SeasonNo { get; set; }
        public int GameweekNo { get; set; }
        public DateTime MatchDateTime { get; set; }
        public TeamModel HomeTeam { get; set; }
        public TeamModel AwayTeam { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
    }
}

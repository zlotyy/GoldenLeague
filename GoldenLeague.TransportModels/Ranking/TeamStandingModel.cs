using System;

namespace GoldenLeague.TransportModels.Ranking
{
    public class TeamStandingModel
    {
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public int SeasonNo { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Defeats { get; set; }
        public int Points { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
    }
}

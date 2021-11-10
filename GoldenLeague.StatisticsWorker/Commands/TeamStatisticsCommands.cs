using AutoMapper;
using GoldenLeague.Common.Localization;
using GoldenLeague.Database;
using GoldenLeague.Database.Queries;
using GoldenLeague.StatisticsWorker.Queries;
using GoldenLeague.TransportModels.Common;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Commands
{
    public interface ITeamStatisticsCommands
    {
        Result<int> UpdateTeamsStatistics();
    }

    public class TeamStatisticsCommands : ITeamStatisticsCommands
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<TeamStatisticsCommands> _logger;
        private readonly IMapper _mapper;
        private readonly ITeamQueries _teamQueries;
        private readonly IBaseQueries _baseQueries;

        public TeamStatisticsCommands(IDbContextFactory dbContextFactory, ILogger<TeamStatisticsCommands> logger, IMapper mapper,
            ITeamQueries teamQueries, IBaseQueries baseQueries)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _mapper = mapper;
            _teamQueries = teamQueries;
            _baseQueries = baseQueries;
        }

        public Result<int> UpdateTeamsStatistics()
        {
            var result = new Result<int>();

            try
            {
                var teams = _teamQueries.GetTeams();
                var seasonNo = _baseQueries.GetCurrentSeasonNo();

                using (var db = _dbContextFactory.Create())
                {
                    var rankRecords = new List<PremierLeagueTable>();

                    teams.ForEach(team =>
                    {
                        var teamId = team.TeamId;
                        var homeMatches =
                            db.Matches.Where(x => x.HomeTeamId == teamId && x.HomeTeamScore.HasValue && x.AwayTeamScore.HasValue).ToList();
                        var awayMatches =
                            db.Matches.Where(x => x.AwayTeamId == teamId && x.HomeTeamScore.HasValue && x.AwayTeamScore.HasValue).ToList();

                        var matchesPlayed = homeMatches.Count + awayMatches.Count;
                        var wins = homeMatches.Count(x => x.HomeTeamScore > x.AwayTeamScore) + awayMatches.Count(x => x.AwayTeamScore > x.HomeTeamScore);
                        var draws = homeMatches.Count(x => x.HomeTeamScore == x.AwayTeamScore) + awayMatches.Count(x => x.AwayTeamScore == x.HomeTeamScore);
                        var defeats = homeMatches.Count(x => x.HomeTeamScore < x.AwayTeamScore) + awayMatches.Count(x => x.AwayTeamScore < x.HomeTeamScore);
                        var points = wins * 3 + draws;
                        var goalsScored = homeMatches.Sum(x => x.HomeTeamScore.Value) + awayMatches.Sum(x => x.AwayTeamScore.Value);
                        var goalsConceded = homeMatches.Sum(x => x.AwayTeamScore.Value) + awayMatches.Sum(x => x.HomeTeamScore.Value);

                        var rankRecord = new PremierLeagueTable
                        {
                            TeamId = teamId,
                            SeasonNo = seasonNo,
                            MatchesPlayed = matchesPlayed,
                            Wins = wins,
                            Draws = draws,
                            Defeats = defeats,
                            Points = points,
                            GoalsScored = goalsScored,
                            GoalsConceded = goalsConceded
                        };

                        rankRecords.Add(rankRecord);
                    });

                    var upsertedRecordsCount = db.PremierLeagueTable
                        .Merge()
                        .Using(rankRecords)
                        .On(x => new { x.TeamId, x.SeasonNo }, x => new { x.TeamId, x.SeasonNo })
                        .UpdateWhenMatchedAnd(
                            (t, s) => t.MatchesPlayed != s.MatchesPlayed || t.GoalsScored != s.GoalsScored || t.GoalsConceded != s.GoalsConceded)
                        .InsertWhenNotMatched()
                        .Merge();

                    result.Data = upsertedRecordsCount;

                    if (upsertedRecordsCount > 0)
                    {
                        _logger.LogInformation($"{nameof(UpdateTeamsStatistics)} finished with {upsertedRecordsCount} upserted records");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpdateTeamsStatistics)}");
                result.Errors.Add(ErrorLocalization.ErrorDBUpsert);
            }

            return result;
        }
    }
}

using GoldenLeague.Database;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.StatisticsWorker.Commands
{
    public interface ITeamCommands
    {
        void UpsertTeams(IEnumerable<Teams> teams);
    }

    public class TeamCommands : ITeamCommands
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<TeamCommands> _logger;

        public TeamCommands(IDbContextFactory dbContextFactory, ILogger<TeamCommands> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public void UpsertTeams(IEnumerable<Teams> teams)
        {
            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    db.Teams
                        .Merge()
                        .Using(teams)
                        .On((t, s) => t.ForeignKey == s.ForeignKey)
                        .InsertWhenNotMatched()
                        .UpdateWhenMatched((t, s) => new Teams
                        {
                            TeamName = s.TeamName,
                            TeamNameShort = s.TeamNameShort,
                            TeamNameAbbreviation = s.TeamNameAbbreviation,
                            CompetitionsId = s.CompetitionsId
                        })
                        .Merge();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpsertTeams)}");
            }
        }
    }
}

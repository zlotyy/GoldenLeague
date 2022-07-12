using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Models.FootballApi.Responses.Fixtures;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldenLeague.StatisticsWorker.Commands
{
    public interface ITeamCommands
    {
        void UpdateTeams(IEnumerable<TeamModel> teams);
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

        public void UpdateTeams(IEnumerable<TeamModel> teams)
        {

        }
    }
}

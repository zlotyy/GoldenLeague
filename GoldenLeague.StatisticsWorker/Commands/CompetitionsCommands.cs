using GoldenLeague.Database;
using GoldenLeague.StatisticsWorker.Services;
using LinqToDB;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

namespace GoldenLeague.StatisticsWorker.Commands
{
    public interface ICompetitionsCommands
    {
        public void UpsertCompetitions(IEnumerable<Competitions> competitions);
    }

    public class CompetitionsCommands : ICompetitionsCommands
    {
        private readonly ILogger<FootballApiService> _logger;
        private readonly IDbContextFactory _dbContextFactory;
        private readonly AppSettings _config;

        public CompetitionsCommands(ILogger<FootballApiService> logger, IOptions<AppSettings> config, IDbContextFactory dbContextFactory)
        {
            _logger = logger;
            _config = config.Value;
            _dbContextFactory = dbContextFactory;
        }

        public void UpsertCompetitions(IEnumerable<Competitions> competitions)
        {
            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    db.Competitions
                        .Merge()
                        .Using(competitions)
                        .On((t, s) => t.ForeignKey == s.ForeignKey)
                        .InsertWhenNotMatched()
                        .UpdateWhenMatched((t, s) => new Competitions
                        {
                            CompetitionsName = s.CompetitionsName,
                            CurrentSeasonNo = s.CurrentSeasonNo,
                            CountryName = s.CountryName,
                            CountryCode = s.CountryCode
                        })
                        .Merge();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpsertCompetitions)}");
            }
        }
    }
}

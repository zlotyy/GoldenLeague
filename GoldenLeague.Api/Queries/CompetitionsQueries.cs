using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface ICompetitionsQueries
    {
        IEnumerable<CompetitionModel> GetBookmakerCompetitionsForUser(Guid userId);
    }

    public class CompetitionsQueries : ICompetitionsQueries
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<CompetitionsQueries> _logger;

        public CompetitionsQueries(IDbContextFactory dbContextFactory, ILogger<CompetitionsQueries> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public IEnumerable<CompetitionModel> GetBookmakerCompetitionsForUser(Guid userId)
        {
            using (var db = _dbContextFactory.Create())
            {
                var data = db.BookmakerLeaguesLUsers
                    .Where(x => x.UserId == userId 
                        && !x.UserLeaveDate.HasValue
                        && !x.League.IsDeleted)
                    .SelectMany(s => s.League.LCompetitions.Select(x => new CompetitionModel
                    {
                        CompetitionsId = x.CompetitionsId,
                        CompetitionsName = x.Competition.CompetitionsName,
                        CompetitionsIcon = x.Competition.CompetitionsIcon,
                        CurrentSeasonNo = x.Competition.CurrentSeasonNo,
                        CountryName = x.Competition.CountryName,
                        CountryCode = x.Competition.CountryCode,
                        CountryIcon = x.Competition.CountryIcon
                    }))
                    .Distinct()
                    .ToList();

                return data;
            }
        }
    }
}

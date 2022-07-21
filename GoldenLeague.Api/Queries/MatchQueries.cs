using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.Database.Queries;
using GoldenLeague.TransportModels.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface IMatchQueries : IBaseQueries
    {
        IEnumerable<MatchResultSimpleModel> GetIncomingBookmakerMatchesForUser(Guid userId, int count);
        [Obsolete]
        IEnumerable<VMatch> GetMatchesFull(int seasonNo, int gameweekNo);
        [Obsolete]
        IEnumerable<VMatch> GetMatchesFull(int seasonNo);
        [Obsolete]
        IEnumerable<VMatch> GetCurrentSeasonMatchesFull();
        [Obsolete]
        IEnumerable<VMatch> GetCurrentGameweekMatchesFull();
    }

    public class MatchQueries : BaseQueries, IMatchQueries
    {
        private readonly ILogger<MatchQueries> _logger;
        private readonly IMapper _mapper;
        private readonly ICompetitionsQueries _competitionsQueries;

        public MatchQueries(IDbContextFactory dbContextFactory, ILogger<MatchQueries> logger,
            IMapper mapper, ICompetitionsQueries competitionsQueries) : base(dbContextFactory)
        {
            _logger = logger;
            _mapper = mapper;
            _competitionsQueries = competitionsQueries;
        }

        public IEnumerable<MatchResultSimpleModel> GetIncomingBookmakerMatchesForUser(Guid userId, int count)
        {
            using (var db = _dbContextFactory.Create())
            {
                var startDate = DateTime.Now.AddHours(1);
                var competitionsIds = _competitionsQueries.GetBookmakerCompetitionsForUser(userId).Select(x => x.CompetitionsId);
                return db.VMatch
                    .Where(x => x.MatchDateTime > startDate && competitionsIds.Contains(x.CompetitionsId))
                    .OrderBy(x => x.MatchDateTime)
                    .Take(count)
                    .Select(x => new MatchResultSimpleModel
                    {
                        MatchId = x.MatchId,
                        MatchDateTime = x.MatchDateTime,
                        HomeTeamId = x.HomeTeamId,
                        AwayTeamId = x.AwayTeamId,
                        HomeTeamName = x.HomeTeamName,
                        AwayTeamName = x.AwayTeamName,
                        HomeTeamScore = x.HomeTeamScore,
                        AwayTeamScore = x.AwayTeamScore
                    })
                    .ToList();
            }
        }

        public IEnumerable<VMatch> GetMatchesFull(int seasonNo, int gameweekNo)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo && x.GameweekNo == gameweekNo).ToList();
            }
        }

        public IEnumerable<VMatch> GetMatchesFull(int seasonNo)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo).OrderBy(o => o.MatchDateTime).ToList();
            }
        }

        public IEnumerable<VMatch> GetCurrentSeasonMatchesFull()
        {
            var seasonNo = GetCurrentSeasonNo();

            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo).OrderBy(o => o.MatchDateTime).ToList();
            }
        }

        public IEnumerable<VMatch> GetCurrentGameweekMatchesFull()
        {
            var seasonNo = GetCurrentSeasonNo();
            var gameweekNo = GetCurrentGameweekNo();

            using (var db = _dbContextFactory.Create())
            {
                return db.VMatch.Where(x => x.SeasonNo == seasonNo && x.GameweekNo == gameweekNo).ToList();
            }
        }
    }
}

using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.StatisticsWorker.Commands
{
    public interface IMatchCommands
    {
        bool UpsertMatches(List<MatchModel> matches);
    }

    public class MatchCommands : IMatchCommands
    {

        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<MatchCommands> _logger;
        private readonly IMapper _mapper;

        public MatchCommands(IDbContextFactory dbContextFactory, ILogger<MatchCommands> logger, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
            _mapper = mapper;
        }

        public bool UpsertMatches(List<MatchModel> matches)
        {
            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var result = db.Matches
                        .Merge()
                        .Using(matches)
                        .On((t, s) => t.ForeignKey == s.ForeignKey)
                        .InsertWhenNotMatched((s) => new Matches
                        {
                            ForeignKey = s.ForeignKey,
                            SeasonNo = s.SeasonNo,
                            GameweekNo = s.GameweekNo,
                            MatchDateTime = s.MatchDateTime,
                            HomeTeamId = s.HomeTeamId,
                            AwayTeamId = s.AwayTeamId,
                            HomeTeamScore = s.HomeTeamScore,
                            AwayTeamScore = s.AwayTeamScore                            
                        })
                        .Merge();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpsertMatches)}");
                return false;
            }
        }
    }
}

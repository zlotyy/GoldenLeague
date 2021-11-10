using AutoMapper;
using GoldenLeague.Common.Localization;
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
        Result<int> InsertNewMatches(List<MatchModel> matches);
        Result<int> UpsertMatchesData(List<MatchModel> matches);
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

        public Result<int> InsertNewMatches(List<MatchModel> matches)
        {
            var result = new Result<int>();

            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var insertedMatchesCount = db.Matches
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

                    if (insertedMatchesCount > 0)
                    {
                        result.Data = insertedMatchesCount;
                        _logger.LogInformation($"{nameof(InsertNewMatches)} finished with {insertedMatchesCount} inserted records");
                        db.CreateMatchBettingRecords();
                        db.SetMatchBettingPointsForEmptyBetting();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(InsertNewMatches)}");
                result.Errors.Add(ErrorLocalization.ErrorDBUpsert);
            }

            return result;
        }

        public Result<int> UpsertMatchesData(List<MatchModel> matches)
        {
            var result = new Result<int>();

            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var upsertedMatchesCount = db.Matches
                        .Merge()
                        .Using(matches)
                        .On((t, s) => t.ForeignKey == s.ForeignKey)
                        .UpdateWhenMatchedAnd(
                            (t, s) => t.MatchDateTime != s.MatchDateTime
                                || t.HomeTeamScore != s.HomeTeamScore
                                || t.AwayTeamScore != s.AwayTeamScore
                                || t.GameweekNo != s.GameweekNo,
                            (t, s) => new Matches
                            {
                                MatchDateTime = s.MatchDateTime,
                                HomeTeamScore = s.HomeTeamScore,
                                AwayTeamScore = s.AwayTeamScore,
                                GameweekNo = s.GameweekNo
                            }
                        )
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

                    result.Data = upsertedMatchesCount;
                    
                    if (upsertedMatchesCount > 0)
                    {
                        _logger.LogInformation($"{nameof(UpsertMatchesData)} finished with {upsertedMatchesCount} upserted records");
                        db.CreateMatchBettingRecords();
                        db.SetMatchBettingPointsForEmptyBetting();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpsertMatchesData)}");
                result.Errors.Add(ErrorLocalization.ErrorDBUpsert);
            }

            return result;
        }
    }
}

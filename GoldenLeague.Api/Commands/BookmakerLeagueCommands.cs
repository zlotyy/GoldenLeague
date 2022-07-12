using GoldenLeague.Database;
using GoldenLeague.TransportModels.Bookmaker;
using GoldenLeague.TransportModels.Common;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace GoldenLeague.Api.Commands
{
    public interface IBookmakerLeagueCommands
    {
        Result<bool> LeagueCreate(LeagueCreateModel model);
        Result<bool> LeagueJoin(LeagueJoinModel model);
        Result<bool> LeagueLeave(LeagueLeaveModel model);
    }

    public class BookmakerLeagueCommands : IBookmakerLeagueCommands
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<BookmakerLeagueCommands> _logger;

        public BookmakerLeagueCommands(IDbContextFactory dbContextFactory, ILogger<BookmakerLeagueCommands> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public Result<bool> LeagueCreate(LeagueCreateModel model)
        {
            var result = new Result<bool>();

            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var now = DateTime.Now;
                    var leagueId = Guid.NewGuid();
                    var leagueCompetitions = model.CompetitionsIds
                        .Select(competitionId => new BookmakerLeaguesLCompetitions
                        {
                            LeagueId = leagueId,
                            CompetitionId = competitionId
                        });

                    using (var transaction = db.BeginTransaction())
                    {
                        db.Insert(new BookmakerLeagues
                        {
                            LeagueId = leagueId,
                            LeagueName = model.Name,
                            InsertUserId = model.InsertUserId,
                            IsPrivate = true,
                            InsertDate = now
                        });

                        db.BulkCopy(leagueCompetitions);

                        db.Insert(new BookmakerLeaguesLUsers
                        {
                            LeagueId = leagueId,
                            UserId = model.InsertUserId,
                            UserJoinDate = now
                        });

                        transaction.Commit();
                    }
                }

                result.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(LeagueCreate)}");
                result.Errors.Add("Wystąpił błąd podczas tworzenia ligi");
            }

            return result;
        }

        public Result<bool> LeagueJoin(LeagueJoinModel model)
        {
            var result = new Result<bool>();

            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var leagueUserLink = db.BookmakerLeaguesLUsers
                        .FirstOrDefault(x => x.LeagueId == model.LeagueId && x.UserId == model.UserId);

                    if (leagueUserLink == null)
                    {
                        db.Insert(new BookmakerLeaguesLUsers
                        {
                            LeagueId = model.LeagueId,
                            UserId = model.UserId,
                            UserJoinDate = DateTime.Now
                        });
                    }
                    else
                    {
                        if (leagueUserLink.UserLeaveDate.HasValue)
                        {
                            db.BookmakerLeaguesLUsers
                                .Where(x => x.LeagueId == model.LeagueId && x.UserId == model.UserId)
                                .Set(x => x.UserLeaveDate, default(DateTime?))
                                .Update();
                        }
                    }
                }

                result.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(LeagueJoin)}");
                result.Errors.Add("Wystąpił błąd podczas próby dołączenia do ligi");
            }

            return result;
        }

        public Result<bool> LeagueLeave(LeagueLeaveModel model)
        {
            var result = new Result<bool>();

            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    var alreadyJoined = db.BookmakerLeaguesLUsers
                        .Any(x => x.LeagueId == model.LeagueId && x.UserId == model.UserId && !x.UserLeaveDate.HasValue);

                    if (alreadyJoined)
                    {
                        db.BookmakerLeaguesLUsers
                            .Where(x => x.LeagueId == model.LeagueId && x.UserId == model.UserId)
                            .Set(x => x.UserLeaveDate, DateTime.Now)
                            .Update();
                    }
                }

                result.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(LeagueLeave)}");
                result.Errors.Add("Wystąpił błąd podczas próby opuszczenia ligi");
            }

            return result;
        }
    }
}

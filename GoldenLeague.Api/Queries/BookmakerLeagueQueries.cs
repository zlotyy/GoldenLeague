using GoldenLeague.Database;
using GoldenLeague.TransportModels.Bookmaker;
using GoldenLeague.TransportModels.Common;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Queries
{
    public interface IBookmakerLeagueQueries
    {
        IEnumerable<EntryLeagueModel> GetJoinedLeagues(Guid userId);
        IEnumerable<CompetitionModel> GetCompetitions();
        LeagueRankModel GetLeagueRanking(Guid leagueId);
        bool LeagueExists(Guid leagueId);
        bool LeagueExists(string name);
        bool LeagueAlreadyJoined(Guid leagueId, Guid userId);
    }

    public class BookmakerLeagueQueries : IBookmakerLeagueQueries
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ILogger<BookmakerLeagueQueries> _logger;

        public BookmakerLeagueQueries(IDbContextFactory dbContextFactory, ILogger<BookmakerLeagueQueries> logger)
        {
            _dbContextFactory = dbContextFactory;
            _logger = logger;
        }

        public IEnumerable<EntryLeagueModel> GetJoinedLeagues(Guid userId)
        {
            using (var db = _dbContextFactory.Create())
            {
                var data = db.BookmakerLeaguesLUsers
                    .LoadWith(x => x.User)
                    .LoadWith(x => x.League)
                        .ThenLoad(x => x.LCompetitions)
                    .LoadWith(x => x.League)
                        .ThenLoad(x => x.InsertUser)
                    .Where(x => x.UserId == userId && !x.UserLeaveDate.HasValue)
                    .Select(x => new EntryLeagueModel
                    {
                        LeagueId = x.LeagueId,
                        LeagueName = x.League.LeagueName,
                        IsPrivate = x.League.IsPrivate,
                        InsertDate = x.League.InsertDate,
                        InsertUserId = x.League.InsertUserId,
                        InsertUserLogin = x.League.InsertUser.Login,
                        Competitions = x.League.LCompetitions.Select(s => new CompetitionModel
                        {
                            CompetitionsId = s.CompetitionId,
                            CompetitionsName = s.Competition.CompetitionsName,
                            CompetitionsIcon = s.Competition.CompetitionsIcon,
                            CountryIcon = s.Competition.CountryIcon,
                            CurrentSeasonNo = s.Competition.CurrentSeasonNo,
                            CurrentGameweekNo = s.Competition.CurrentGameweekNo
                        }),
                        EntryRank = x.UserRanking
                    })
                    .ToList();

                return data;
            }
        }

        public IEnumerable<CompetitionModel> GetCompetitions()
        {
            using (var db = _dbContextFactory.Create())
            {
                var data = db.Competitions
                    .Select(x => new CompetitionModel
                    {
                        CompetitionsId = x.CompetitionsId,
                        CompetitionsName = x.CompetitionsName,
                        CompetitionsIcon = x.CompetitionsIcon,
                        CountryIcon = x.CountryIcon,
                        CurrentSeasonNo = x.CurrentSeasonNo,
                        CurrentGameweekNo = x.CurrentGameweekNo
                    })
                    .ToList();

                return data;
            }
        }

        public LeagueRankModel GetLeagueRanking(Guid leagueId)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.VBookmakerLeagueRank
                    .Where(x => x.LeagueId == leagueId && x.UserRanking.HasValue)
                    .OrderBy(x => x.UserRanking)
                    .GroupBy(x => x.LeagueId)
                    .DisableGuard()
                    .Select(x => new LeagueRankModel
                    {
                        LeagueId = x.First().LeagueId,
                        LeagueName = x.First().LeagueName,
                        SeasonNo = x.First().SeasonNo,
                        Users = x.Select(s => new LeagueRankRecordModel
                        {
                            UserId = s.UserId,
                            UserLogin = s.UserLogin,
                            UserRanking = s.UserRanking.Value,
                            UserPoints = s.UserPoints,
                            UserCorrectBets = s.UserCorrectBets,
                            UserCorrectResults = s.UserCorrectResults
                        }).ToList()
                    })
                    .FirstOrDefault();
            }
        }

        public bool LeagueExists(Guid leagueId)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.BookmakerLeagues.Any(x => x.LeagueId == leagueId && !x.IsDeleted);
            }
        }

        public bool LeagueExists(string name)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.BookmakerLeagues.Any(x => x.LeagueName == name && !x.IsDeleted);
            }
        }

        public bool LeagueAlreadyJoined(Guid leagueId, Guid userId)
        {
            using (var db = _dbContextFactory.Create())
            {
                return db.BookmakerLeaguesLUsers.Any(x => x.LeagueId == leagueId && x.UserId == userId && !x.UserLeaveDate.HasValue);
            }
        }
    }
}

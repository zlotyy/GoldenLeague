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
        IEnumerable<LeagueModel> GetJoinedLeagues(Guid userId);
        IEnumerable<CompetitionModel> GetCompetitions();
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

        public IEnumerable<LeagueModel> GetJoinedLeagues(Guid userId)
        {
            using (var db = _dbContextFactory.Create())
            {
                var data = db.BookmakerLeaguesLUsers
                    .LoadWith(x => x.User)
                    .LoadWith(x => x.League)
                        .ThenLoad(x => x.LCompetitions)
                    .Where(x => x.UserId == userId && !x.UserLeaveDate.HasValue)
                    .Select(x => new LeagueModel
                    {
                        LeagueId = x.LeagueId,
                        LeagueName = x.League.LeagueName,
                        IsPrivate = x.League.IsPrivate,
                        InsertDate = x.League.InsertDate,
                        InsertUserId = x.League.InsertUserId,
                        Competitions = x.League.LCompetitions.Select(s => new CompetitionModel
                        {
                            CompetitionsId = s.CompetitionId,
                            CompetitionsName = s.Competition.CompetitionsName,
                            CompetitionsIcon = s.Competition.CompetitionsIcon,
                            CountryIcon = s.Competition.CountryIcon,
                            CurrentSeasonNo = s.Competition.CurrentSeasonNo,
                            CurrentGameweekNo = s.Competition.CurrentGameweekNo
                        })
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

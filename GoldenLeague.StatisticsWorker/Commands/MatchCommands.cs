using AutoMapper;
using GoldenLeague.Database;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.StatisticsWorker.Commands
{
    public interface IMatchCommands
    {
        void UpsertMatches(IEnumerable<Matches> matches);
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

        public void UpsertMatches(IEnumerable<Matches> matches)
        {
            try
            {
                using (var db = _dbContextFactory.Create())
                {
                    db.Matches
                        .Merge()
                        .Using(matches)
                        .On((t, s) => t.ForeignKey == s.ForeignKey)
                        .InsertWhenNotMatched()
                        .UpdateWhenMatchedAnd(
                            (t, s) => 
                                t.MatchDateTime != s.MatchDateTime
                                 || t.GameweekNo != s.GameweekNo                            
                                 || t.HomeTeamScore != s.HomeTeamScore
                                 || t.AwayTeamScore != s.AwayTeamScore
                                 || t.IsFinished != s.IsFinished,
                            (t, s) => new Matches
                            {
                                MatchDateTime = s.MatchDateTime,
                                GameweekNo = s.GameweekNo,
                                HomeTeamScore = s.HomeTeamScore,
                                AwayTeamScore = s.AwayTeamScore,
                                IsFinished = s.IsFinished
                            })
                        .Merge();

                    // trigger powinien uzupełnić wpisy w BookmakerBets dla nowych meczów dla każdego użytkownika
                    // TODO: Trigger nie działa, na razie zostaje procedura ale pomyśleć nad tym - podejrzec w triggerze Competitions
                    db.CreateBookmakerBetRecords();

                    // uzupełnij punkty w BookmakerBets dla ukończonych meczów, które mają jeszcze nieuzupełnione punkty
                    db.SetBookmakerBetPointsForFinishedMatches();

                    // uzupełnij punkty i pozycje dla wszystkich lig BookmakerLeagues
                    db.SetBookmakerLeaguesResults();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpsertMatches)}");
            }
        }
    }
}

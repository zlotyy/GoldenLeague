using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.MatchBetting;

namespace GoldenLeague.Api.Automapper
{
    public class BookmakerBetProfile : Profile
    {
        public BookmakerBetProfile()
        {
            // Note: Do not use with ProjectTo
            CreateMap<VBookmakerBet, BookmakerBetModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(d => d.Match, o => o.MapFrom<MatchResolver>())
                .ForMember(d => d.MatchResultBet, o => o.MapFrom<MatchResultBetResolver>());
        }

        public class MatchResolver : IValueResolver<VBookmakerBet, BookmakerBetModel, MatchFullModel>
        {
            public MatchFullModel Resolve(VBookmakerBet source, BookmakerBetModel destination, MatchFullModel destMember, ResolutionContext context)
            {
                var homeTeam = new TeamModel(source.HomeTeamId, source.HomeForeignKey, source.HomeTeamName, source.HomeTeamNameShort, source.HomeTeamNameAbbreviation);
                var awayTeam = new TeamModel(source.AwayTeamId, source.AwayForeignKey, source.AwayTeamName, source.AwayTeamNameShort, source.AwayTeamNameAbbreviation);
                return new MatchFullModel(source.MatchId, source.SeasonNo, source.GameweekNo, source.MatchDateTime,
                    homeTeam, awayTeam, source.HomeTeamScoreActual, source.AwayTeamScoreActual);
            }
        }

        public class MatchResultBetResolver : IValueResolver<VBookmakerBet, BookmakerBetModel, MatchResultBetModel>
        {
            public MatchResultBetModel Resolve(VBookmakerBet s, BookmakerBetModel destination, MatchResultBetModel destMember, ResolutionContext context)
            {
                BetResultEnum? bettingResult = s.BettingPoints == 0 ? BetResultEnum.MISSED
                    : s.BettingPoints == 1 ? BetResultEnum.PARTIALLY_HIT
                    : s.BettingPoints == 3 ? BetResultEnum.HIT
                    : null;

                return new MatchResultBetModel(s.HomeTeamScoreBet, s.AwayTeamScoreBet, s.BettingPoints, bettingResult);
            }
        }
    }
}

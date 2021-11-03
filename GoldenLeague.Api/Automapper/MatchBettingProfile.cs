using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.MatchBetting;

namespace GoldenLeague.Api.Automapper
{
    public class MatchBettingProfile : Profile
    {
        public MatchBettingProfile()
        {
            // Note: Do not use with ProjectTo
            CreateMap<VMatchBetting, MatchBettingModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId))
                .ForMember(d => d.Match, o => o.MapFrom<MatchResolver>())
                .ForMember(d => d.MatchResultBet, o => o.MapFrom<MatchResultBetResolver>());
        }

        public class MatchResolver : IValueResolver<VMatchBetting, MatchBettingModel, MatchFullModel>
        {
            public MatchFullModel Resolve(VMatchBetting source, MatchBettingModel destination, MatchFullModel destMember, ResolutionContext context)
            {
                var homeTeam = new TeamModel(source.HomeTeamId, source.HomeForeignKey, source.HomeTeamName, source.HomeTeamNameShort, source.HomeTeamNameAbbreviation);
                var awayTeam = new TeamModel(source.AwayTeamId, source.AwayForeignKey, source.AwayTeamName, source.AwayTeamNameShort, source.AwayTeamNameAbbreviation);
                return new MatchFullModel(source.MatchId, source.SeasonNo, source.GameweekNo, source.MatchDateTime,
                    homeTeam, awayTeam, source.HomeTeamScoreActual, source.AwayTeamScoreActual);
            }
        }

        public class MatchResultBetResolver : IValueResolver<VMatchBetting, MatchBettingModel, MatchResultBetModel>
        {
            public MatchResultBetModel Resolve(VMatchBetting s, MatchBettingModel destination, MatchResultBetModel destMember, ResolutionContext context)
            {
                BettingResultEnum? bettingResult = s.BettingPoints == 0 ? BettingResultEnum.MISSED
                    : s.BettingPoints == 1 ? BettingResultEnum.PARTIALLY_HIT
                    : s.BettingPoints == 3 ? BettingResultEnum.HIT
                    : null;

                return new MatchResultBetModel(s.HomeTeamScoreBet, s.AwayTeamScoreBet, s.BettingPoints, bettingResult);
            }
        }
    }
}

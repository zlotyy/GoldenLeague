using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.MatchBetting;

namespace GoldenLeague.Api.Automapper
{
    public class MatchBettingProfile : Profile
    {
        public MatchBettingProfile()
        {
            // Note: Do not use with ProjectTo
            CreateMap<VMatchBetting, MatchBettingModel>()
                .ForMember(d => d.MatchResult, o => o.MapFrom<MatchResultResolver>());
        }

        public class MatchResultResolver : IValueResolver<VMatchBetting, MatchBettingModel, MatchResultModel>
        {
            public MatchResultModel Resolve(VMatchBetting s, MatchBettingModel destination, MatchResultModel destMember, ResolutionContext context)
            {
                BettingResultEnum? bettingResult = s.BettingPoints == 0 ? BettingResultEnum.MISSED
                    : s.BettingPoints == 1 ? BettingResultEnum.PARTIALLY_HIT
                    : s.BettingPoints == 3 ? BettingResultEnum.HIT
                    : null;

                return new MatchResultModel(
                    new TeamMatchDetailsModel(s.HomeTeamId, s.HomeTeamName, s.HomeTeamScoreBet, s.HomeTeamScoreActual),
                    new TeamMatchDetailsModel(s.AwayTeamId, s.AwayTeamName, s.AwayTeamScoreBet, s.AwayTeamScoreActual),
                    s.BettingPoints,
                    bettingResult
                );
            }
        }
    }
}

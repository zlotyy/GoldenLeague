using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;

namespace GoldenLeague.Api.Automapper
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            // Note: Do not use with ProjectTo
            CreateMap<VMatch, MatchFullModel>()
                .ForMember(d => d.MatchId, o => o.MapFrom(s => s.MatchId))
                .ForMember(d => d.ForeignKey, o => o.MapFrom(s => s.ForeignKey))
                .ForMember(d => d.SeasonNo, o => o.MapFrom(s => s.SeasonNo))
                .ForMember(d => d.GameweekNo, o => o.MapFrom(s => s.GameweekNo))
                .ForMember(d => d.MatchDateTime, o => o.MapFrom(s => s.MatchDateTime))
                .ForMember(d => d.HomeTeamScore, o => o.MapFrom(s => s.HomeTeamScore))
                .ForMember(d => d.AwayTeamScore, o => o.MapFrom(s => s.AwayTeamScore))
                .ForMember(d => d.HomeTeam, o => o.MapFrom<HomeTeamResolver>())
                .ForMember(d => d.AwayTeam, o => o.MapFrom<AwayTeamResolver>());
        }

        public class HomeTeamResolver : IValueResolver<VMatch, MatchFullModel, TeamModel>
        {
            public TeamModel Resolve(VMatch source, MatchFullModel destination, TeamModel destMember, ResolutionContext context)
            {
                return new TeamModel(source.HomeTeamId, source.HomeForeignKey, source.HomeTeamName, source.HomeTeamNameShort, source.HomeTeamNameAbbreviation);
            }
        }

        public class AwayTeamResolver : IValueResolver<VMatch, MatchFullModel, TeamModel>
        {
            public TeamModel Resolve(VMatch source, MatchFullModel destination, TeamModel destMember, ResolutionContext context)
            {
                return new TeamModel(source.AwayTeamId, source.AwayForeignKey, source.AwayTeamName, source.AwayTeamNameShort, source.AwayTeamNameAbbreviation);
            }
        }
    }
}

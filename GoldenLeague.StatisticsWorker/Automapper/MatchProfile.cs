using AutoMapper;
using GoldenLeague.StatisticsWorker.Models.Fantasy;
using GoldenLeague.TransportModels.Common;

namespace GoldenLeague.StatisticsWorker.Automapper
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<FixtureMatchModel, MatchModel>()
                .ForMember(d => d.ForeignKey, o => o.MapFrom(s => s.Code))
                .ForMember(d => d.SeasonNo, o => o.MapFrom((s, d, m, context) => context.Items["SeasonNo"]))
                .ForMember(d => d.GameweekNo, o => o.MapFrom(s => s.Event))
                .ForMember(d => d.MatchDateTime, o => o.MapFrom(s => s.Kickoff_Time.ToLocalTime()))
                .ForMember(d => d.HomeTeamFK, o => o.MapFrom(s => s.Team_H))
                .ForMember(d => d.AwayTeamFK, o => o.MapFrom(s => s.Team_A))
                .ForMember(d => d.HomeTeamScore, o => o.MapFrom(s => s.Team_H_Score))
                .ForMember(d => d.AwayTeamScore, o => o.MapFrom(s => s.Team_A_Score));
        }
    }
}

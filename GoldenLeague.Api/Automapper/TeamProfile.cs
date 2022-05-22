using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Ranking;

namespace GoldenLeague.Api.Automapper
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Teams, TeamModel>()
                .ForMember(d => d.TeamId, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.ForeignKey, o => o.MapFrom(s => s.ForeignKey))
                .ForMember(d => d.TeamName, o => o.MapFrom(s => s.TeamName))
                .ForMember(d => d.TeamNameShort, o => o.MapFrom(s => s.TeamNameShort))
                .ForMember(d => d.TeamNameAbbreviation, o => o.MapFrom(s => s.TeamNameAbbreviation));

            CreateMap<PremierLeagueTable, TeamStandingModel>()
                .ForMember(d => d.TeamId, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.TeamName, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.SeasonNo, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.MathesPlayed, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.Wins, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.Draws, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.Defeats, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.Points, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.GoalsScored, o => o.MapFrom(s => s.TeamId))
                .ForMember(d => d.GoalsConceded, o => o.MapFrom(s => s.TeamId));
        }
    }
}

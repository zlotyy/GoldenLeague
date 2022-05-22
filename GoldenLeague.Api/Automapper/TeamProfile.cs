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
                .ForMember(d => d.TeamName, o => o.MapFrom(s => s.Team.TeamName))
                .ForMember(d => d.SeasonNo, o => o.MapFrom(s => s.SeasonNo))
                .ForMember(d => d.MatchesPlayed, o => o.MapFrom(s => s.MatchesPlayed))
                .ForMember(d => d.Wins, o => o.MapFrom(s => s.Wins))
                .ForMember(d => d.Draws, o => o.MapFrom(s => s.Draws))
                .ForMember(d => d.Defeats, o => o.MapFrom(s => s.Defeats))
                .ForMember(d => d.Points, o => o.MapFrom(s => s.Points))
                .ForMember(d => d.GoalsScored, o => o.MapFrom(s => s.GoalsScored))
                .ForMember(d => d.GoalsConceded, o => o.MapFrom(s => s.GoalsConceded));
        }
    }
}

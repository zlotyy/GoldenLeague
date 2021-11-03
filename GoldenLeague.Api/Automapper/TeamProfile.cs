using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;

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
        }
    }
}

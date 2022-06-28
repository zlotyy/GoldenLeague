using AutoMapper;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Bookmaker;

namespace GoldenLeague.Api.Automapper
{
    public class BookmakerBetProfile : Profile
    {
        public BookmakerBetProfile()
        {
            //// Note: Do not use with ProjectTo
            //CreateMap<VBookmakerBet, BookmakerBetRecord>()
            //    .ForMember(d => d.Match, o => o.MapFrom<MatchResolver>())
            //    .ForMember(d => d.MatchResultBet, o => o.MapFrom<MatchResultBetResolver>());
        }

        //public class MatchResolver : IValueResolver<VBookmakerBet, BookmakerBetRecord, MatchFullModel>
        //{
        //    public MatchFullModel Resolve(VBookmakerBet source, BookmakerBetRecord destination, MatchFullModel destMember, ResolutionContext context)
        //    {
        //        var homeTeam = new TeamModel(source.HomeTeamId, source.HomeForeignKey, source.HomeTeamName, source.HomeTeamNameShort, source.HomeTeamNameAbbreviation);
        //        var awayTeam = new TeamModel(source.AwayTeamId, source.AwayForeignKey, source.AwayTeamName, source.AwayTeamNameShort, source.AwayTeamNameAbbreviation);
        //        return new MatchFullModel(source.MatchId, source.SeasonNo, source.GameweekNo, source.MatchDateTime,
        //            homeTeam, awayTeam, source.HomeTeamScoreActual, source.AwayTeamScoreActual);
        //    }
        //}

        //public class MatchResultBetResolver : IValueResolver<VBookmakerBet, BookmakerBetRecord, MatchResultBetModel>
        //{
        //    public MatchResultBetModel Resolve(VBookmakerBet s, BookmakerBetRecord destination, MatchResultBetModel destMember, ResolutionContext context)
        //    {
        //        return new MatchResultBetModel(s.HomeTeamScoreBet, s.AwayTeamScoreBet, s.BettingPoints);
        //    }
        //}
    }
}

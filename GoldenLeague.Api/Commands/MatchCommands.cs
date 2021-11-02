using AutoMapper;
using GoldenLeague.Api.Queries;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoldenLeague.Api.Commands
{
    public interface IMatchCommands
    {
        bool UpsertMatches(List<MatchModel> matches);
    }

    public class MatchCommands : IMatchCommands
    {

        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMatchQueries _queries;
        private readonly ILogger<MatchCommands> _logger;
        private readonly IMapper _mapper;

        public MatchCommands(IDbContextFactory dbContextFactory, IMatchQueries queries, ILogger<MatchCommands> logger, IMapper mapper)
        {
            _dbContextFactory = dbContextFactory;
            _queries = queries;
            _logger = logger;
            _mapper = mapper;
        }

        public bool UpsertMatches(List<MatchModel> matches)
        {
            try
            {
                var seasonNo = _queries.GetCurrentSeasonNo();
                var allMatches = _queries.GetCurrentSeasonMatches();
                using (var db = _dbContextFactory.Create())
                {
                    var existingMatches = (from a in matches
                                           join b in allMatches
                                                on new { HomeForeignKey = a.HomeTeam.ForeignKey, AwayForeignKey = a.AwayTeam.ForeignKey } 
                                                equals new { b.HomeForeignKey, b.AwayForeignKey }
                                           select b).ToList();
                    // TODO
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpsertMatches)}");
                return false;
            }
        }
    }
}

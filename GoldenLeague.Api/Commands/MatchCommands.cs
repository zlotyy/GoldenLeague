using AutoMapper;
using GoldenLeague.Api.Queries;
using GoldenLeague.Database;
using GoldenLeague.TransportModels.Common;
using LinqToDB;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Api.Commands
{
    public interface IMatchCommands
    {
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
    }
}

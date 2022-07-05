using AutoMapper;
using GoldenLeague.Api.Queries;
using GoldenLeague.Common.Localization;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Ranking;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Api.Controllers
{
    public class TeamsController : BaseController
    {
        private readonly ILogger<TeamsController> _logger;
        private readonly ITeamQueries _queries;
        private readonly IMapper _mapper;

        public TeamsController(ILogger<TeamsController> logger, ITeamQueries queries, IMapper mapper)
        {
            _logger = logger;
            _queries = queries;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetTeams()
        {
            var result = new Result<IEnumerable<TeamModel>>(new List<TeamModel>());

            try
            {
                var data = _queries.GetTeams();
                var mappedData = _mapper.Map<List<TeamModel>>(data);
                result.Data = mappedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetTeams)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }

        // TODO USUNAC
        [HttpGet("ranking")]
        public IActionResult GetRanking()
        {
            var result = new Result<IEnumerable<TeamStandingModel>>(new List<TeamStandingModel>());

            try
            {
                var data = _queries.GetTeamsStandings();
                var mappedData = _mapper.Map<List<TeamStandingModel>>(data);
                result.Data = mappedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetRanking)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }
    }
}

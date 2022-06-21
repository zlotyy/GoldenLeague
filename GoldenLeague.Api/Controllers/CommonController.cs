using GoldenLeague.Api.Queries;
using GoldenLeague.Common.Localization;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Api.Controllers
{
    public class CommonController : BaseController
    {
        private readonly ILogger<CommonController> _logger;
        private readonly IBookmakerLeagueQueries _leagueQueries;

        public CommonController(ILogger<CommonController> logger, IBookmakerLeagueQueries leagueQueries)
        {
            _logger = logger;
            _leagueQueries = leagueQueries;
        }

        [HttpGet("competitions")]
        public IActionResult GetCompetitions()
        {
            var result = new Result<IEnumerable<CompetitionModel>>(new List<CompetitionModel>());

            try
            {
                result.Data = _leagueQueries.GetCompetitions();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCompetitions)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
                return InternalServerError(result);
            }

            return Ok(result);
        }
    }
}

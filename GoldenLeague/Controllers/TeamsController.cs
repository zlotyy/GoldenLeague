using GoldenLeague.Common.Localization;
using GoldenLeague.Common.Services;
using GoldenLeague.Database.Queries;
using GoldenLeague.Helpers;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Ranking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GoldenLeague.Controllers
{
    [AllowAnonymous]
    public class TeamsController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<TeamsController> _logger;
        private readonly IBaseQueries _queries;

        public TeamsController(IRestService restService, ILogger<TeamsController> logger, IBaseQueries queries)
        {
            _restService = restService;
            _logger = logger;
            _queries = queries;
        }

        [HttpGet("ranking")]
        public IActionResult GetPremierLeagueRanking()
        {
            var response = _restService.Get<Result<IEnumerable<TeamStandingModel>>>(ApiUrlHelper.TeamsRanking);
            if (!response.IsSuccessful)
            {
                var result = new Result<IEnumerable<TeamStandingModel>>(new List<TeamStandingModel>(), new List<string> { ErrorLocalization.ErrorAPIUnknown });
                return Ok(result);
            }
            return Ok(response.Data);
        }
    }
}

using GoldenLeague.Common.Localization;
using GoldenLeague.Common.Services;
using GoldenLeague.Database.Queries;
using GoldenLeague.Helpers;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace GoldenLeague.Controllers
{
    [AllowAnonymous]    // TODO Usunąć
    public class MatchesController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<MatchesController> _logger;
        private readonly IBaseQueries _queries;

        public MatchesController(IRestService restService, ILogger<MatchesController> logger, IBaseQueries queries)
        {
            _restService = restService;
            _logger = logger;
            _queries = queries;
        }

        [HttpGet("current-gameweek-no")]
        public IActionResult GetCurrentGameweekNo()
        {
            var gameweek = _queries.GetCurrentGameweekNo();
            return Ok(gameweek);
        }

        [HttpGet("current-gameweek")]
        public IActionResult GetCurrentGameweekMatches()
        {
            var response = _restService.Get<Result<List<MatchModel>>>(ApiUrlHelper.MatchesCurrentGameweek);
            if (!response.IsSuccessful)
            {
                var result = new Result<List<MatchModel>>(new List<MatchModel>(), new List<string> { ErrorLocalization.ErrorAPIUnknown });
                return Ok(result);
            }
            return Ok(response.Data);
        }
    }
}

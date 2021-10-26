using GoldenLeague.Database.Queries;
using GoldenLeague.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        [HttpGet("current-gameweek")]
        public IActionResult GetCurrentGameweekNo()
        {
            var gameweek = _queries.GetCurrentGameweek();
            return Ok(gameweek);
        }
    }
}

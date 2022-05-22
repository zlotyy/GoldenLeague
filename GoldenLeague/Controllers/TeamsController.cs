using GoldenLeague.Common.Services;
using GoldenLeague.Database.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet("standings")]
        public IActionResult GetCurrentGameweekNo()
        {
            var gameweek = _queries.GetCurrentGameweekNo();
            return Ok(gameweek);
        }
    }
}

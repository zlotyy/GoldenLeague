using GoldenLeague.Common.Services;
using GoldenLeague.Helpers;
using GoldenLeague.TransportModels.Bookmaker;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GoldenLeague.Controllers
{
    public class BookmakerLeaguesController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<UsersController> _logger;

        public BookmakerLeaguesController(IRestService restService, ILogger<UsersController> logger)
        {
            _restService = restService;
            _logger = logger;
        }

        [HttpGet("{id}/rank")]
        public IActionResult GetLeagueRank([FromRoute] Guid id)
        {
            try
            {
                var response = _restService.Get<Result<LeagueRankModel>>(ApiUrlHelper.BookmakerLeagueRank(id));
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetLeagueRank)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult CreateLeague([FromBody] LeagueCreateModel model)
        {
            try
            {
                var response = _restService.Post<Result<bool>>(ApiUrlHelper.BookmakerLeaguesBase, model);
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(CreateLeague)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

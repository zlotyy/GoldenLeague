using GoldenLeague.Api.Commands;
using GoldenLeague.Api.Queries;
using GoldenLeague.Common.Localization;
using GoldenLeague.TransportModels.Bookmaker;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace GoldenLeague.Api.Controllers
{
    [Route("api/v1/bookmaker-leagues")]
    public class BookmakerLeaguesController : BaseController
    {
        private readonly ILogger<BookmakerLeaguesController> _logger;
        private readonly IBookmakerLeagueQueries _leagueQueries;
        private readonly IBookmakerLeagueCommands _leagueCommands;

        public BookmakerLeaguesController(ILogger<BookmakerLeaguesController> logger, IBookmakerLeagueQueries leagueQueries,
            IBookmakerLeagueCommands leagueCommands)
        {
            _logger = logger;
            _leagueQueries = leagueQueries;
            _leagueCommands = leagueCommands;
        }

        [HttpGet("{id}/rank")]
        public IActionResult GetLeagueRank([FromRoute] Guid id)
        {
            var result = new Result<LeagueRankModel>();

            try
            {
                var data = _leagueQueries.GetLeagueRanking(id);
                if (data == null)
                {
                    result.Errors.Add("Nie znaleziono ligi");
                    return NotFound(result);
                }

                result.Data = data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetLeagueRank)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateLeague([FromBody] LeagueCreateModel model)
        {
            var result = new Result<bool>();
            if (_leagueQueries.LeagueExists(model.Name))
            {
                result.Errors.Add("Liga prywatna o takiej nazwie już istnieje");
                return BadRequest(result);
            }

            result = _leagueCommands.LeagueCreate(model);
            if (!result.Success)
            {
                return InternalServerError(result);
            }

            return Ok(result);
        }
    }
}

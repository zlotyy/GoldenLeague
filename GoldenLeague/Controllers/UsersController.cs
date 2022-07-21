using GoldenLeague.Common.Localization;
using GoldenLeague.Common.Services;
using GoldenLeague.Database.Enums;
using GoldenLeague.Database.Queries;
using GoldenLeague.Helpers;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Bookmaker;
using GoldenLeague.TransportModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<UsersController> _logger;
        private readonly IBaseQueries _queries;

        private readonly int _currentSeasonNo;

        public UsersController(IRestService restService, ILogger<UsersController> logger, IBaseQueries queries)
        {
            _restService = restService;
            _logger = logger;
            _queries = queries;

            _currentSeasonNo = int.Parse(_queries.GetConfigValue(ConfigKeys.CURRENT_SEASON_NO));
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateModel model)
        {
            try
            {
                var response = _restService.Post<Result<UserModel>>(ApiUrlHelper.UsersBase, model);
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(CreateUser)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}/bookmaker-bets")]
        public IActionResult GetMatchBetting([FromRoute] Guid id)
        {
            try
            {
                var response = _restService.Get<Result<List<BookmakerUserBetsModel>>>(ApiUrlHelper.UserBookmakerBetsGet(id));
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetMatchBetting)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{id}/bookmaker-bets")]
        public IActionResult UpdateBookmakerBets([FromRoute] Guid id, [FromBody] List<BookmakerBetRecord> model)
        {
            try
            {
                var response = _restService.Patch<Result<bool>>(ApiUrlHelper.UserBookmakerBetsUpdate(id), model);
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpdateBookmakerBets)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}/bookmaker-leagues-joined")]
        public IActionResult GetBookmakerLeaguesJoined([FromRoute] Guid id)
        {
            try
            {
                var response = _restService.Get<Result<IEnumerable<EntryLeagueModel>>>(ApiUrlHelper.UserBookmakerLeaguesJoined(id));
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerLeaguesJoined)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/bookmaker-league-join")]
        public IActionResult BookmakerLeagueJoin([FromRoute] Guid id, [FromBody] LeagueJoinModel model)
        {
            try
            {
                var response = _restService.Post<Result<bool>>(ApiUrlHelper.UserBookmakerLeagueJoin(id), model);
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(BookmakerLeagueJoin)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}/bookmaker-league-leave")]
        public IActionResult BookmakerLeagueLeave([FromRoute] Guid id, [FromBody] LeagueLeaveModel model)
        {
            try
            {
                var response = _restService.Post<Result<bool>>(ApiUrlHelper.UserBookmakerLeagueLeave(id), model);
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(BookmakerLeagueLeave)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}/bookmaker-competitions")]
        public IActionResult GetBookmakerCompetitions([FromRoute] Guid id)
        {
            try
            {
                var response = _restService.Get<Result<List<CompetitionModel>>>(ApiUrlHelper.UserBookmakerCompetitions(id));
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerCompetitions)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}/bookmaker-incoming-matches")]
        public IActionResult GetBookmakerIncomingMatches([FromRoute] Guid id)
        {
            try
            {
                var response = _restService.Get<Result<List<MatchResultSimpleModel>>>(ApiUrlHelper.UserBookmakerIncomingMatches(id));
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerIncomingMatches)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

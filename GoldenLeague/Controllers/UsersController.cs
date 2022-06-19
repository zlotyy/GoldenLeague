using GoldenLeague.Common.Localization;
using GoldenLeague.Common.Services;
using GoldenLeague.Database.Enums;
using GoldenLeague.Database.Queries;
using GoldenLeague.Helpers;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.MatchBetting;
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

        [HttpGet("{id}/bookmaker-bets")]
        public IActionResult GetMatchBetting([FromRoute] Guid id)
        {
            var response = _restService.Get<Result<List<BookmakerBetModel>>>(ApiUrlHelper.UserBookmakerBetsGet(id, _currentSeasonNo));
            if (!response.IsSuccessful)
            {
                var result = new Result<List<BookmakerBetModel>>(new List<BookmakerBetModel>(), new List<string> { ErrorLocalization.ErrorAPIUnknown });
                return Ok(result);
            }
            return Ok(response.Data);
        }

        [HttpPatch("{id}/bookmaker-bets")]
        public IActionResult UpdateBookmakerBets([FromRoute] Guid id, [FromBody] List<BookmakerBetModel> model)
        {
            var response = _restService.Patch<Result<bool>>(ApiUrlHelper.UserBookmakerBetsUpdate(id), model);
            if (!response.IsSuccessful)
            {
                var result = new Result<bool>(new List<string> { ErrorLocalization.ErrorAPIUnknown });
                return Ok(result);
            }
            return Ok(response.Data);
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
    }
}

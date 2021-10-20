﻿using GoldenLeague.Database.Enums;
using GoldenLeague.Database.Queries;
using GoldenLeague.Services;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.MatchBetting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Controllers
{
    [AllowAnonymous]    // TODO Usunąć
    public class UsersController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<MatchBettingController> _logger;
        private readonly IBaseQueries _queries;

        private readonly int _currentSeasonNo;

        public UsersController(IRestService restService, ILogger<MatchBettingController> logger, IBaseQueries queries)
        {
            _restService = restService;
            _logger = logger;
            _queries = queries;

            _currentSeasonNo = int.Parse(_queries.GetConfigValue(ConfigKeys.CURRENT_SEASON_NO));
        }

        [HttpGet("{id}/match-betting")]
        public IActionResult GetMatchBetting([FromRoute] Guid id)
        {
            var response = _restService.Get<Result<List<MatchBettingModel>>>($"users/{id}/match-betting/?seasonNo={_currentSeasonNo}");
            if (!response.IsSuccessful)
            {
                var result = new Result<List<MatchBettingModel>>(new List<MatchBettingModel>(), new List<string> { "COMMON ERROR - TODO Localization" });
                return Ok(result);
            }
            return Ok(response.Data);
        }
    }
}

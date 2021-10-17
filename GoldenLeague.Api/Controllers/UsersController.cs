using GoldenLeague.Api.Queries;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.MatchBetting;
using GoldenLeague.TransportModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserQueries _userQueries;
        private readonly IMatchBettingQueries _matchBettingQueries;

        public UsersController(ILogger<UsersController> logger, IUserQueries userQueries, IMatchBettingQueries matchBettingQueries)
        {
            _logger = logger;
            _userQueries = userQueries;
            _matchBettingQueries = matchBettingQueries;
        }

        [HttpPost]
        public UserModel GetUser([FromBody] UserCredentials credentials)
        {
            _logger.LogDebug($"Request {nameof(GetUser)}, login: {credentials.Login}");
            var user = _userQueries.GetUser(credentials);
            var userModel = user != null ? new UserModel(user) : null;
            return userModel;
        }

        [HttpGet("{id}/match-betting/{seasonNo}")]
        public IActionResult GetMatchBetting([FromRoute] Guid id, [FromRoute] int seasonNo)
        {
            var data = _matchBettingQueries.GetMatchBetting(id, seasonNo);
            var result = new Result<IEnumerable<MatchBettingModel>>(data);

            return Ok(result);
        }
    }
}

using AutoMapper;
using GoldenLeague.Api.Commands;
using GoldenLeague.Api.Queries;
using GoldenLeague.Common.Localization;
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
        private readonly IMatchBettingCommands _matchBettingCommands;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUserQueries userQueries, IMatchBettingQueries matchBettingQueries,
            IMatchBettingCommands matchBettingCommands, IMapper mapper)
        {
            _logger = logger;
            _userQueries = userQueries;
            _matchBettingQueries = matchBettingQueries;
            _matchBettingCommands = matchBettingCommands;
            _mapper = mapper;
        }

        [HttpPost]
        public UserModel GetUser([FromBody] UserCredentials credentials)
        {
            _logger.LogDebug($"Request {nameof(GetUser)}, login: {credentials.Login}");
            var user = _userQueries.GetUser(credentials);
            var userModel = user != null ? new UserModel(user) : null;
            return userModel;
        }

        [HttpGet("{id}/match-betting")]
        public IActionResult GetMatchBetting([FromRoute] Guid id, [FromQuery] int seasonNo)
        {
            var result = new Result<IEnumerable<MatchBettingModel>>(new List<MatchBettingModel>());

            try
            {
                var data = _matchBettingQueries.GetMatchBetting(id, seasonNo);
                var mappedData = _mapper.Map<List<MatchBettingModel>>(data);
                result.Data = mappedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetMatchBetting)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }

        [HttpPatch("{id}/match-betting")]
        public IActionResult UpdateMatchBetting([FromRoute] Guid id, [FromBody] List<MatchBettingModel> model)
        {
            var result = new Result<bool>();

            try
            {
                var updateResult = _matchBettingCommands.UpdateMatchBetting(model);
                if (!updateResult)
                {
                    result.Errors.Add(ErrorLocalization.ErrorDBUpsert);
                }
                else
                {
                    result.Data = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetMatchBetting)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }
    }
}

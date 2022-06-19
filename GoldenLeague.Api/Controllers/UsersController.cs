using AutoMapper;
using GoldenLeague.Api.Commands;
using GoldenLeague.Api.Queries;
using GoldenLeague.Common.Localization;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.MatchBetting;
using GoldenLeague.TransportModels.Users;
using Microsoft.AspNetCore.Http;
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
        private readonly IUserCommands _userCommands;
        private readonly IBookmakerBetQueries _bookmakerBetQueries;
        private readonly IBookmakerBetCommands _bookmakerBetCommands;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUserQueries userQueries, IUserCommands userCommands,
            IBookmakerBetQueries bookmakerBetQueries, IBookmakerBetCommands bookmakerBetCommands, IMapper mapper)
        {
            _logger = logger;
            _userQueries = userQueries;
            _bookmakerBetQueries = bookmakerBetQueries;
            _bookmakerBetCommands = bookmakerBetCommands;
            _mapper = mapper;
            _userCommands = userCommands;
        }

        [HttpPost("authenticate")]
        public IActionResult GetUser([FromBody] UserCredentials credentials)
        {
            var result = new Result<UserModel>();
            var user = _userQueries.GetUser(credentials.Login);
            
            if (user == null)
            {
                result.Errors.Add("Nie znaleziono użytkownika o podanym loginie");
                return NotFound(result);
            }

            // TODO - PasswordHash
            if (user.Password != credentials.Password)
            {
                result.Errors.Add("Podane hasło jest nieprawidłowe");
                return Unauthorized(result);
            }

            result.Data = new UserModel
            {
                UserId = user.UserId,
                Login = user.Login,
                FullName = user.FullName,
                IsAdmin = user.IsAdmin
            };

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateModel model)
        {
            var result = new Result<UserModel>();
            if (_userQueries.UserExists(model.Login))
            {
                result.Errors.Add("Użytkownik o takim loginie już istnieje");
                return BadRequest(result);
            }

            result = _userCommands.UserCreate(model);
            if (!result.Success)
            {
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}/bookmaker-bets")]
        public IActionResult GetBookmakerBets([FromRoute] Guid id, [FromQuery] int seasonNo)
        {
            var result = new Result<IEnumerable<BookmakerBetModel>>(new List<BookmakerBetModel>());

            try
            {
                var data = _bookmakerBetQueries.GetBets(id, seasonNo);
                var mappedData = _mapper.Map<List<BookmakerBetModel>>(data);
                result.Data = mappedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerBets)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }

        [HttpPatch("{id}/bookmaker-bets")]
        public IActionResult UpdateBookmakerBet([FromRoute] Guid id, [FromBody] List<BookmakerBetModel> model)
        {
            var result = new Result<bool>();

            try
            {
                var updateResult = _bookmakerBetCommands.UpdateBet(model);
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
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerBets)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }
    }
}

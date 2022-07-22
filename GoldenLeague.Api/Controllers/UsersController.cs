using AutoMapper;
using GoldenLeague.Api.Commands;
using GoldenLeague.Api.Queries;
using GoldenLeague.Common.Localization;
using GoldenLeague.TransportModels.Common;
using GoldenLeague.TransportModels.Bookmaker;
using GoldenLeague.TransportModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using GoldenLeague.Database;
using GoldenLeague.Api.Helpers;

namespace GoldenLeague.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserQueries _userQueries;
        private readonly IUserCommands _userCommands;
        private readonly IBookmakerBetQueries _bookmakerBetQueries;
        private readonly IBookmakerLeagueQueries _bookmakerLeagueQueries;
        private readonly ICompetitionsQueries _competitionsQueries;
        private readonly IMatchQueries _matchQueries;
        private readonly IBookmakerBetCommands _bookmakerBetCommands;
        private readonly IBookmakerLeagueCommands _bookmakerLeagueCommands;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, IUserQueries userQueries, IUserCommands userCommands,
            IBookmakerBetQueries bookmakerBetQueries, IBookmakerBetCommands bookmakerBetCommands, IMapper mapper, 
            IBookmakerLeagueQueries bookmakerLeagueQueries, IBookmakerLeagueCommands bookmakerLeagueCommands,
            ICompetitionsQueries competitionsQueries, IMatchQueries matchQueries)
        {
            _logger = logger;
            _userQueries = userQueries;
            _bookmakerBetQueries = bookmakerBetQueries;
            _bookmakerBetCommands = bookmakerBetCommands;
            _mapper = mapper;
            _userCommands = userCommands;
            _bookmakerLeagueQueries = bookmakerLeagueQueries;
            _bookmakerLeagueCommands = bookmakerLeagueCommands;
            _competitionsQueries = competitionsQueries;
            _matchQueries = matchQueries;
        }

        [HttpPost("authenticate")]
        public IActionResult AuthUser([FromBody] UserCredentials credentials)
        {
            var result = new Result<UserModel>();
            var user = _userQueries.GetUser(credentials.Login);
            
            if (user == null)
            {
                result.Errors.Add("Nie znaleziono użytkownika o podanym loginie");
                return NotFound(result);
            }

            if (user.Password != PasswordHelpers.GetHash(credentials.Password, user.PasswordSalt))
            {
                result.Errors.Add("Podane hasło jest nieprawidłowe");
                return Unauthorized(result);
            }

            result.Data = new UserModel
            {
                UserId = user.UserId,
                Login = user.Login,
                Email = user.Email,
                IsAdmin = user.IsAdmin
            };

            return Ok(result);
        }

        [HttpGet("{id}/exists")]
        public IActionResult UserExists([FromRoute] Guid id)
        {
            var result = new Result<bool>();
            try
            {
                var user = _userQueries.UserExists(id);

                if (!user)
                {
                    result.Errors.Add("Nie znaleziono użytkownika o podanym id");
                    return NotFound(result);
                }

                result.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UserExists)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
                return InternalServerError(result);
            }

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
        public IActionResult GetBookmakerBets([FromRoute] Guid id)
        {
            var result = new Result<IEnumerable<BookmakerUserBetsModel>>(new List<BookmakerUserBetsModel>());

            try
            {
                var data = _bookmakerBetQueries.GetUserBets(id);
                var competitionsGroupedData = data
                    .GroupBy(x => x.CompetitionsId)
                    .Select(x => new BookmakerUserBetsModel
                    {
                        CompetitionsId = x.Key,
                        UserBets = x.Select(s => new BookmakerBetRecord
                        {
                            Match = new MatchFullModel(
                                s.MatchId,
                                s.SeasonNo,
                                s.GameweekNo,
                                s.MatchDateTime,
                                new TeamModel(s.HomeTeamId, s.HomeForeignKey, s.HomeTeamName, s.HomeTeamNameShort, s.HomeTeamNameAbbreviation),
                                new TeamModel(s.AwayTeamId, s.AwayForeignKey, s.AwayTeamName, s.AwayTeamNameShort, s.AwayTeamNameAbbreviation),
                                s.HomeTeamScoreActual,
                                s.AwayTeamScoreActual),
                            MatchResultBet = new MatchResultBetModel(
                                s.HomeTeamScoreBet,
                                s.AwayTeamScoreBet,
                                s.BettingPoints)
                        })
                    })
                    .ToList();

                result.Data = competitionsGroupedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerBets)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpPatch("{id}/bookmaker-bets")]
        public IActionResult UpdateBookmakerBet([FromRoute] Guid id, [FromBody] List<BookmakerBetRecord> model)
        {
            var result = new Result<bool>();

            try
            {
                var updateResult = _bookmakerBetCommands.UpdateBets(model, id);
                if (!updateResult)
                {
                    result.Errors.Add(ErrorLocalization.ErrorDBUpsert);
                    return InternalServerError(result);
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
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}/bookmaker-leagues-joined")]
        public IActionResult GetBookmakerLeaguesJoined([FromRoute] Guid id)
        {
            var result = new Result<IEnumerable<EntryLeagueModel>>(new List<EntryLeagueModel>());

            try
            {
                result.Data = _bookmakerLeagueQueries.GetJoinedLeagues(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerLeaguesJoined)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpPost("{id}/bookmaker-league-join")]
        public IActionResult JoinLeague([FromBody] LeagueJoinModel model)
        {
            var leagueExists = _bookmakerLeagueQueries.LeagueExists(model.LeagueId);
            if (!leagueExists)
            {
                return BadRequest(new Result<bool>($"Liga o identyfikatorze '{model.LeagueId}' nie istnieje"));
            }

            var leagueJoined = _bookmakerLeagueQueries.LeagueAlreadyJoined(model.LeagueId, model.UserId);
            if (leagueJoined)
            {
                return BadRequest(new Result<bool>("Dołączyłeś już do tej ligi"));
            }

            var result = _bookmakerLeagueCommands.LeagueJoin(model);
            if (!result.Success)
            {
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpPost("{id}/bookmaker-league-leave")]
        public IActionResult LeaveLeague([FromBody] LeagueLeaveModel model)
        {
            var result = _bookmakerLeagueCommands.LeagueLeave(model);
            if (!result.Success)
            {
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}/bookmaker-competitions")]
        public IActionResult GetBookmakerCompetitions([FromRoute] Guid id)
        {
            var result = new Result<IEnumerable<CompetitionModel>>(new List<CompetitionModel>());

            try
            {
                result.Data = _competitionsQueries.GetBookmakerCompetitionsForUser(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerCompetitions)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
                return InternalServerError(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}/bookmaker-incoming-matches")]
        public IActionResult GetBookmakerIncomingMatches([FromRoute] Guid id)
        {
            var result = new Result<IEnumerable<MatchResultSimpleModel>>(new List<MatchResultSimpleModel>());

            try
            {
                result.Data = _matchQueries.GetIncomingBookmakerMatchesForUser(id, 20);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetBookmakerIncomingMatches)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
                return InternalServerError(result);
            }

            return Ok(result);
        }
    }
}

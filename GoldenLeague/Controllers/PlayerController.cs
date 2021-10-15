using GoldenLeague.Models;
using GoldenLeague.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Controllers
{
    [AllowAnonymous]    // TODO Usunąć
    public class PlayerController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<MatchBettingController> _logger;

        public PlayerController(IRestService restService, ILogger<MatchBettingController> logger)
        {
            _restService = restService;
            _logger = logger;
        }

        [HttpGet("{id}/match-bettings")]
        public IActionResult GetMatchBettings([FromRoute] Guid id)
        {
            var data = new List<MatchBettingModel>
            {
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 16, 13, 30, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Watford", null, null),
                    new TeamBetDetails(Guid.NewGuid(), "Liverpool", null, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 16, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Man City", 2, null),
                    new TeamBetDetails(Guid.NewGuid(), "Burnley", 0, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 16, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Norwich", 1, null),
                    new TeamBetDetails(Guid.NewGuid(), "Brighton", 2, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 16, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Leicester", 1, null),
                    new TeamBetDetails(Guid.NewGuid(), "Man Utd", 1, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 16, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Aston Villa", 2, null),
                    new TeamBetDetails(Guid.NewGuid(), "Wolves", 1, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 16, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Southampton", 1, null),
                    new TeamBetDetails(Guid.NewGuid(), "Leeds", 1, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 16, 18, 30, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Brentford", 0, null),
                    new TeamBetDetails(Guid.NewGuid(), "Chelsea", 1, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 17, 15, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Everton", 0, null),
                    new TeamBetDetails(Guid.NewGuid(), "West Ham", 1, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 17, 17, 30, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Newcastle", 1, null),
                    new TeamBetDetails(Guid.NewGuid(), "Tottenham", 1, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 18, 21, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Arsenal", 2, null),
                    new TeamBetDetails(Guid.NewGuid(), "Crystal Palace", 1, null)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 2, 13, 30, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Man Utd", 1, 1),
                    new TeamBetDetails(Guid.NewGuid(), "Everton", 1, 1)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 2, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Wolves", 2, 2),
                    new TeamBetDetails(Guid.NewGuid(), "Newcastle", 0, 1)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 2, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Leeds", 2, 1),
                    new TeamBetDetails(Guid.NewGuid(), "Watford", 1, 0)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 2, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Burnley", 1, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Norwich", 1, 0)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 2, 18, 30, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Chelsea", 2, 3),
                    new TeamBetDetails(Guid.NewGuid(), "Southampton", 0, 1)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 3, 15, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Brighton", 1, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Arsenal", 2, 0)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 3, 15, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Crystal Palace", 0, 2),
                    new TeamBetDetails(Guid.NewGuid(), "Leicester", 1, 2)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 3, 15, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Tottenham", 1, 2),
                    new TeamBetDetails(Guid.NewGuid(), "Aston Villa", 1, 1)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 3, 17, 30, 0),
                    new TeamBetDetails(Guid.NewGuid(), "West Ham", 1, 1),
                    new TeamBetDetails(Guid.NewGuid(), "Brentford", 0, 2)
                ),
                new MatchBettingModel(
                    id,
                    Guid.NewGuid(),
                    new DateTime(2021, 10, 2, 16, 0, 0),
                    new TeamBetDetails(Guid.NewGuid(), "Liverpool", 1, 2),
                    new TeamBetDetails(Guid.NewGuid(), "Man City", 1, 2)
                ),
            };

            var result = new Result<List<MatchBettingModel>>(data);

            return Ok(result);
        }
    }
}

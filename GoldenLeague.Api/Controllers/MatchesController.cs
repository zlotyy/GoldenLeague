using AutoMapper;
using GoldenLeague.Api.Queries;
using GoldenLeague.Common.Localization;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Api.Controllers
{
    public class MatchesController : BaseController
    {
        private readonly ILogger<MatchesController> _logger;
        private readonly IMatchQueries _queries;
        private readonly IMapper _mapper;

        private readonly int _currentSeasonNo;
        private readonly int _currentGameweekNo;

        public MatchesController(ILogger<MatchesController> logger, IMatchQueries queries, IMapper mapper)
        {
            _logger = logger;
            _queries = queries;
            _mapper = mapper;

            _currentSeasonNo = _queries.GetCurrentSeasonNo();
            _currentGameweekNo = _queries.GetCurrentGameweekNo();
        }

        [HttpGet]
        public IActionResult GetMatches([FromQuery] int seasonNo, [FromQuery] int gameweekNo)
        {
            var result = new Result<IEnumerable<MatchModel>>(new List<MatchModel>());

            try
            {
                var data = _queries.GetMatches(seasonNo, gameweekNo);
                var mappedData = _mapper.Map<List<MatchModel>>(data);
                result.Data = mappedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetMatches)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }

        [HttpGet("current-season")]
        public IActionResult GetCurrentSeasonMatches()
        {
            var result = new Result<IEnumerable<MatchModel>>(new List<MatchModel>());

            try
            {
                var data = _queries.GetMatches(_currentSeasonNo);
                var mappedData = _mapper.Map<List<MatchModel>>(data);
                result.Data = mappedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCurrentSeasonMatches)}");
                result.Errors.Add(ErrorLocalization.ErrorDBGet);
            }

            return Ok(result);
        }

        [HttpGet("current-gameweek")]
        public IActionResult GetCurrentGameweekMatches()
        {
            return GetMatches(_currentSeasonNo, _currentGameweekNo);
        }
    }
}

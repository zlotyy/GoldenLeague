using GoldenLeague.Common.Services;
using GoldenLeague.Helpers;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GoldenLeague.Controllers
{
    public class CommonController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<CommonController> _logger;

        public CommonController(IRestService restService, ILogger<CommonController> logger)
        {
            _restService = restService;
            _logger = logger;
        }

        [HttpGet("competitions")]
        public IActionResult GetCompetitions()
        {
            try
            {
                var response = _restService.Get<Result<IEnumerable<CompetitionModel>>>(ApiUrlHelper.Competitions);
                return ResolveApiResponse(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCompetitions)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

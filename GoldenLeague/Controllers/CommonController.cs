using GoldenLeague.Common.Services;
using GoldenLeague.Helpers;
using GoldenLeague.Models.Common;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

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

        [HttpGet("competitions-select")]
        public IActionResult GetCompetitionsSelect()
        {
            try
            {
                var response = _restService.Get<Result<IEnumerable<CompetitionModel>>>(ApiUrlHelper.Competitions);
                if (!response.IsSuccessful)
                {
                    return ResolveApiError<IEnumerable<CompetitionModel>, IEnumerable<SelectModel<Guid>>>(response);
                }

                var selectModel = response.Data.Data.Select(x => new SelectModel<Guid>
                {
                    Id = x.CompetitionId,
                    Value = x.CompetitionName
                });

                var result = new Result<IEnumerable<SelectModel<Guid>>>(selectModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCompetitionsSelect)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

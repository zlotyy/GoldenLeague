using GoldenLeague.Common.Services;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoldenLeague.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IRestService restService, ILogger<HomeController> logger)
        {
            _restService = restService;
            _logger = logger;
        }

        [HttpGet("/api-test")]
        public IActionResult ApiTest()
        {
            _logger.LogTrace($"Executed {nameof(ApiTest)} Method");
            var response = _restService.Get<Result<string>>("/");
            if (!response.IsSuccessful)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            return Ok(response.Data);
        }
    }
}

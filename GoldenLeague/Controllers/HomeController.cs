using GoldenLeague.Common.Services;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GoldenLeague.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<HomeController> _logger;
        private readonly AppSettings _settings;

        public HomeController(IRestService restService, ILogger<HomeController> logger, IOptions<AppSettings> settings)
        {
            _restService = restService;
            _logger = logger;
            _settings = settings.Value;
        }

        [HttpGet("/api-test")]
        public IActionResult ApiTest()
        {
            _logger.LogTrace($"Executed {nameof(ApiTest)} Method");
            var response = _restService.Get<Result<string>>("/");
            if (!response.IsSuccessful)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(response.Data);
        }
    }
}

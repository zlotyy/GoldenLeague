using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GoldenLeague.Api.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/api/v1")]
        public IActionResult Ping()
        {
            _logger.LogTrace("Executed Ping Method");
            return Ok(new Result<string> { Data = "API Works!" });
        }
    }
}

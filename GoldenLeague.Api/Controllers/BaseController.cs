using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GoldenLeague.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {
        protected IActionResult InternalServerError<T>(Result<T> result)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        protected IActionResult InternalServerError(IEnumerable<string> messages)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, messages);
        }

        protected IActionResult InternalServerError(string message)
        {
            return InternalServerError(new List<string> { message});
        }

        protected IActionResult InternalServerError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

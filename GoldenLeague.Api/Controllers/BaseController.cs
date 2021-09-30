using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoldenLeague.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoldenLeague.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
    }
}

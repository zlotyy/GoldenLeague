using GoldenLeague.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoldenLeague.TransportModels.Users;
using GoldenLeague.Common.Services;

namespace GoldenLeague.Controllers
{
    public class LoginController : BaseController
    {
        private string _apiUrl(string endpoint = "") => $"/api/v1/user/{endpoint}";
        private readonly IJwtAuthenticationManager _authManager;
        private readonly IRestService _restService;
        private readonly ILogger<LoginController> _logger;

        public LoginController(IJwtAuthenticationManager authManager, IRestService restService, ILogger<LoginController> logger)
        {
            _authManager = authManager;
            _restService = restService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserCredentials model)
        {
            var userResult = _restService.Post<UserModel>(_apiUrl(), model);
            if (!userResult.IsSuccessful)
            {
                return BadRequest();
            }

            var user = userResult.Data;
            if (user == null)
            {
                return Unauthorized();
            }

            var userId = user.UserId;
            var token = _authManager.CreateToken(userId.ToString());
            return Ok(token);
        }
    }
}

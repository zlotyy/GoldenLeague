using GoldenLeague.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoldenLeague.TransportModels.Users;
using GoldenLeague.Common.Services;
using Microsoft.AspNetCore.Http;
using GoldenLeague.Helpers;
using GoldenLeague.Models.Users;

namespace GoldenLeague.Controllers
{
    public class LoginController : BaseController
    {
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
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCredentials model)
        {
            var userResult = _restService.Post<UserModel>(ApiUrlHelper.UsersAuthenticate, model);
            if (!userResult.IsSuccessful)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            var user = userResult.Data;
            if (user == null)
            {
                return NotFound();
            }

            var token = _authManager.CreateToken(user.UserId.ToString());
            var userModel = new UserAuthenticatedModel(user, token);

            return Ok(userModel);
        }
    }
}

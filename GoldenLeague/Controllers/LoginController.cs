using GoldenLeague.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GoldenLeague.TransportModels.Users;
using GoldenLeague.Common.Services;
using Microsoft.AspNetCore.Http;
using GoldenLeague.Helpers;
using GoldenLeague.Models.Users;
using System;
using GoldenLeague.TransportModels.Common;

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

        // TODO - odświeżanie logowania powoduje błąd z kodem 405
        //[HttpGet]
        //[AllowAnonymous]
        //public IActionResult Get()
        //{
        //    return Ok();
        //}

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody] UserCredentials model)
        {
            try
            {
                var response = _restService.Post<Result<UserModel>>(ApiUrlHelper.UsersAuthenticate, model);
                if (!response.IsSuccessful)
                {
                    return ResolveApiError<UserModel, UserAuthenticatedModel>(response);
                }

                var user = response.Data.Data;
                var token = _authManager.CreateToken(user.UserId.ToString());
                var userModel = new UserAuthenticatedModel(user, token);
                var result = new Result<UserAuthenticatedModel>(userModel);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(Authenticate)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

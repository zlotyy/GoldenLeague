using AutoMapper;
using GoldenLeague.TransportModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Collections.Generic;

namespace GoldenLeague.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
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
            return InternalServerError(new List<string> { message });
        }

        protected IActionResult InternalServerError()
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        protected IActionResult ResolveApiResponse<T>(IRestResponse<Result<T>> response)
        {
            var result = response.Data;

            if (!response.Data.Success)
            {
                if ((int)response.StatusCode == StatusCodes.Status400BadRequest) return BadRequest(result);
                else if ((int)response.StatusCode == StatusCodes.Status401Unauthorized) return Unauthorized(result);
                else if ((int)response.StatusCode == StatusCodes.Status404NotFound) return NotFound(result);
                else return InternalServerError(result);
            }

            return Ok(result);
        }

        protected IActionResult ResolveApiError<T, TResult>(IRestResponse<Result<T>> response)
        {
            var result = new Result<TResult> { Errors = response.Data.Errors };

            if ((int)response.StatusCode == StatusCodes.Status400BadRequest) return BadRequest(result);
            else if ((int)response.StatusCode == StatusCodes.Status401Unauthorized) return Unauthorized(result);
            else if ((int)response.StatusCode == StatusCodes.Status404NotFound) return NotFound(result);
            else return InternalServerError(result);
        }

        //protected IActionResult ResolveApiResponse<T, TResult>(IRestResponse<Result<T>> response)
        //{
        //    var result = new Result<TResult>();

        //    if (!response.Data.Success)
        //    {
        //        if ((int)response.StatusCode == StatusCodes.Status400BadRequest) return BadRequest(result);
        //        else if ((int)response.StatusCode == StatusCodes.Status401Unauthorized) return Unauthorized(result);
        //        else if ((int)response.StatusCode == StatusCodes.Status404NotFound) return NotFound(result);
        //        else return InternalServerError(result);
        //    }

        //    // TODO zmapowany obiekt
        //    //model = _map
        //    return Ok(result);
        //}
    }
}

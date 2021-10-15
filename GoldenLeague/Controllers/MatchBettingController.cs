using GoldenLeague.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldenLeague.Controllers
{
    public class MatchBettingController : BaseController
    {
        private readonly IRestService _restService;
        private readonly ILogger<MatchBettingController> _logger;

        public MatchBettingController(IRestService restService, ILogger<MatchBettingController> logger)
        {
            _restService = restService;
            _logger = logger;
        }
    }
}

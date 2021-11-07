using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogProxyApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LogProxyApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HeartbeatController : ControllerBase
    {
        private readonly ILogger<HeartbeatController> _logger;

        public HeartbeatController(ILogger<HeartbeatController> logger)
        {
            this._logger = logger;
        }


        [HttpGet]
        public HeartbeatResponse Get()
        {
            _logger.LogInformation("Beating");
            return new HeartbeatResponse{Status = "beating"};
        }
    }
}
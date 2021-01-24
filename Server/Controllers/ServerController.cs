using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServerController : ControllerBase
    {

        private readonly ILogger<ServerController> _logger;

        public ServerController(ILogger<ServerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String Get()
        {
            string response = "I'm Server";
            using (var wb = new WebClient())
            {
                var requestResponse = wb.DownloadString("http://localhost:62773/User");
                return $"{response}, {requestResponse}";
            }
        }
    }
}

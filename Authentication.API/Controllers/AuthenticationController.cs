using Authentication.API.BusinessLogic;
using Authentication.API.Models;
using Authentication.API.S3Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogin _login;
        private readonly IS3Service _s3Service;

        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger,ILogin login,IS3Service s3Service)
        {
            _logger = logger;
            _login = login;
            _s3Service = s3Service;
        }

        /// <summary>
        /// Authenticate User
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPost]
        
        [Route("AuthenticateUser")]
        public async Task<ActionResult> AuthenticateUser([FromBody] UserModel userRequest)
        {
            var result = await _login.AuthenticateUser(userRequest);
            if(result == null || !result.IsAuthenticated)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("GetFiles")]
        public async Task<ActionResult> GetFiles()
        {
            return Ok(await _s3Service.GetFiles());
        }
    }
}

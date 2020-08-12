using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_core_api_react.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet("github-oauth")]
        public ActionResult Get()
        {
            return Ok("success");
        }
        [HttpGet("login")]
        public ActionResult Login()
        {
            return Challenge("GitHub");
        }
    }
}

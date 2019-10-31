using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActuaPollsBackend.Models;
using ActuaPollsBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ActuaPollsBackend.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}
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
        private readonly PollsContext _context;
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Email or password is incorrect" });

            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]User userParam)
        {
            String status = _userService.Register(userParam.Username, userParam.Email, userParam.Password);

            if (status == "username")
                return Ok(new { message = "username" });

            if (status == "email")
                return Ok(new { message = "email" });

            return Ok(new { message = "succes" });
        }
    }
}
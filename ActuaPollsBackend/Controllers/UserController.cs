using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActuaPollsBackend.Models;
using ActuaPollsBackend.Services;
using Microsoft.AspNetCore.Authorization;
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
                return BadRequest(new { message = "username taken" });

            if (status == "email")
                return BadRequest(new { message = "email taken" });

            return Ok(new { message = "succes" });
        }

        // GET: api/Vote/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }


    }
}
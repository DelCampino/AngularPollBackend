using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActuaPollsBackend.Models;
using ActuaPollsBackend.Services;
using ActuaPollsBackend.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace ActuaPollsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PollsContext _context;
        private IUserService _userService;

        public UserController(PollsContext context, IUserService userService)
        {
            _context = context;
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

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .Include(f => f.RequestSend)
                .ThenInclude(friends => friends.User)
                .ToListAsync();
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users
                .FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.UserID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        [Authorize]
        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        [Authorize]
        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }

        // GET: api/Users/UserWithPolls/5
        // GET methode om user met polls op te halen waar deze in deelneemt.
        [Authorize]
        [HttpGet("UserWithPolls/{id}")]
        public ActionResult<User> GetUserWithPolls(long id)
        {
            var user = _context.Users
                .Where(u => u.UserID == id)
                .Include(u => u.MyPolls)
                .ThenInclude(myPolls => myPolls.Poll)
                .ThenInclude(poll => poll.Answers)
                .ThenInclude(answers => answers.Votes)
                .Include(u => u.MyPolls)
                .ThenInclude(myPolls => myPolls.Poll)
                .ThenInclude(poll => poll.Participants)
                .SingleOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet("count")]
        public async Task<int> GetUsersCount()
        {
            return _context.Users.Count();
        }
    }
}

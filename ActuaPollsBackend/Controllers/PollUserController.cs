using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActuaPollsBackend.Models;

namespace ActuaPollsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollUserController : ControllerBase
    {
        private readonly PollsContext _context;

        public PollUserController(PollsContext context)
        {
            _context = context;
        }

        // GET: api/PollUser
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PollUser>>> GetPollUsers()
        {
            return await _context.PollUsers.ToListAsync();
        }

        // GET: api/PollUser/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PollUser>> GetPollUser(long id)
        {
            var pollUser = await _context.PollUsers.FindAsync(id);

            if (pollUser == null)
            {
                return NotFound();
            }

            return pollUser;
        }

        // PUT: api/PollUser/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPollUser(long id, PollUser pollUser)
        {
            if (id != pollUser.PollUserID)
            {
                return BadRequest();
            }

            _context.Entry(pollUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollUserExists(id))
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

        // POST: api/PollUser
        [HttpPost]
        public async Task<ActionResult<PollUser>> PostPollUser(PollUser pollUser)
        {
            _context.PollUsers.Add(pollUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPollUser", new { id = pollUser.PollUserID }, pollUser);
        }

        // DELETE: api/PollUser/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PollUser>> DeletePollUser(long id)
        {
            var pollUser = await _context.PollUsers.FindAsync(id);
            if (pollUser == null)
            {
                return NotFound();
            }

            _context.PollUsers.Remove(pollUser);
            await _context.SaveChangesAsync();

            return pollUser;
        }

        private bool PollUserExists(long id)
        {
            return _context.PollUsers.Any(e => e.PollUserID == id);
        }
    }
}

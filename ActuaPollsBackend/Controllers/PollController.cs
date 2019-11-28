using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActuaPollsBackend.Models;
using Microsoft.AspNetCore.Authorization;

namespace ActuaPollsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly PollsContext _context;

        public PollController(PollsContext context)
        {
            _context = context;
        }

        // GET: api/Poll
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Poll>>> GetPolls()
        {
            return await _context.Polls
                .Include(p => p.Participants)
                .ThenInclude(participants => participants.User)
                .Include(a => a.Answers)
                .ThenInclude(answers => answers.Votes)
                .ToListAsync();
        }

        // GET: api/Poll/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Poll>> GetPoll(long id)
        {
            var poll = await _context.Polls
                .Where(p => p.PollID == id)
                .Include(p => p.Participants)
                .ThenInclude(participants => participants.User)
                .Include(a => a.Answers)
                .ThenInclude(answers => answers.Votes)
                .SingleOrDefaultAsync();

            if (poll == null)
            {
                return NotFound();
            }

            return poll;
        }

        // PUT: api/Poll/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoll(long id, Poll poll)
        {
            if (id != poll.PollID)
            {
                return BadRequest();
            }

            _context.Entry(poll).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PollExists(id))
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

        // POST: api/Poll
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Poll>> PostPoll(Poll poll)
        {

            _context.Polls.Add(poll);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoll", new { id = poll.PollID }, poll);
        }

        // DELETE: api/Poll/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Poll>> DeletePoll(long id)
        {
            var poll = await _context.Polls.FindAsync(id);
            if (poll == null)
            {
                return NotFound();
            }

            _context.Polls.Remove(poll);
            await _context.SaveChangesAsync();

            return poll;
        }

        [Authorize]
        private bool PollExists(long id)
        {
            return _context.Polls.Any(e => e.PollID == id);
        }

        // GET: api/Polls/CreatedPolls/5
        [Authorize]
        [HttpGet("CreatedPolls/{id}")]
        public async Task<ActionResult<IEnumerable<Poll>>> GetCreatedPolls(long id)
        {
            return await _context.Polls
                .Where(p => p.CreatorID == id)
                .Include(p => p.Participants)
                .ThenInclude(participants => participants.User)
                .Include(a => a.Answers)
                .ThenInclude(answers => answers.Votes)
                .ToListAsync();
        }

        // GET: api/Polls
        [Authorize]
        [HttpGet("count")]
        public async Task<int> GetPollsCount()
        {
            return _context.Polls.Count();
        }
    }
}

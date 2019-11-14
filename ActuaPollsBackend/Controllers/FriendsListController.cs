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
    public class FriendsListController : ControllerBase
    {
        private readonly PollsContext _context;

        public FriendsListController(PollsContext context)
        {
            _context = context;
        }

        // GET: api/FriendsList
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FriendsList>>> GetFriendsList()
        {
            return await _context.FriendsList.ToListAsync();
        }

        // GET: api/FriendsList/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FriendsList>> GetFriendsList(long id)
        {
            var friendsList = await _context.FriendsList.FindAsync(id);

            if (friendsList == null)
            {
                return NotFound();
            }

            return friendsList;
        }

        // PUT: api/FriendsList/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendsList(long id, FriendsList friendsList)
        {
            if (id != friendsList.FriendsListID)
            {
                return BadRequest();
            }

            _context.Entry(friendsList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendsListExists(id))
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

        // POST: api/FriendsList
        [HttpPost]
        public async Task<ActionResult<FriendsList>> PostFriendsList(FriendsList friendsList)
        {
            _context.FriendsList.Add(friendsList);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendsList", new { id = friendsList.FriendsListID }, friendsList);
        }

        // DELETE: api/FriendsList/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FriendsList>> DeleteFriendsList(long id)
        {
            var friendsList = await _context.FriendsList.FindAsync(id);
            if (friendsList == null)
            {
                return NotFound();
            }

            _context.FriendsList.Remove(friendsList);
            await _context.SaveChangesAsync();

            return friendsList;
        }

        private bool FriendsListExists(long id)
        {
            return _context.FriendsList.Any(e => e.FriendsListID == id);
        }

        // PUT: api/ConfirmRequest/5
        [HttpPut("confirm/{id}")]
        public async Task<IActionResult> ConfirmRequest(long id)
        {
            var friendsList = await _context.FriendsList.FindAsync(id);
            if (friendsList == null)
            {
                return NotFound();
            }

            _context.Entry(friendsList).State = EntityState.Modified;

            if (friendsList.Status == false)
            {
                friendsList.Status = true;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendsListExists(id))
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActuaPollsBackend.Models;
using System.Security.Claims;
using ActuaPollsBackend.Models.Dto;

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

        // POST: api/addFriend/5
        [HttpPost("addFriendrequest")]
        public async Task<ActionResult<FriendsList>> addFriendrequest([FromBody]FriendsList_Friendrequest param)
        {
            var friend = _context.Users
                .Where(x => x.Email == param.Email)
                .FirstOrDefault();
            if (friend == null)
            {
                return BadRequest(new { message = "Email not found" });
            }

            if (param.UserID == friend.UserID)
            {
                return BadRequest(new { message = "Cannot add yourself" });
            }

            var friendsList =  _context.FriendsList
                .Where(x => x.UserID == param.UserID)
                .Where(x => x.FriendID == friend.UserID)
                .FirstOrDefault();

            if (friendsList != null)
            {
                if (friendsList.Status == false)
                {
                    return BadRequest(new { message = "Already exists" });
                }
                else
                {
                    return BadRequest(new { message = "Already friends" });
                }
            }

            var friendRequest = new FriendsList
            {
                UserID = param.UserID,
                FriendID = friend.UserID
            };

            _context.FriendsList.Add(friendRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFriendsList", new { id = friendRequest.FriendsListID }, friendRequest);
        }

        // PUT: api/ConfirmRequest/5
        [HttpGet("confirm/{id}")]
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

        // GET: api/FriendsList/Friendrequests/5
        // GET methode om niet-geaccepteerde vriendenrequests op te halen van een specifieke user.
        [HttpGet("Friendrequests/{id}")]
        public async Task<ActionResult<IEnumerable<FriendsList>>> GetFriendRequests(long id)
        {
            var friendrequests = _context.FriendsList
                .Where(fl => (fl.FriendID == id) && (fl.Status == false))
                .Include(fl => fl.User)
                .ToListAsync();

            return await friendrequests;
        }

        // GET: api/FriendsList/5
        // GET methode om een vriendenlijst van een specifieke user op te halen.
        [HttpGet("Friends/{id}")]
        public async Task<ActionResult<IEnumerable<FriendsList>>> GetFriends(long id)
        {
            var friends = _context.FriendsList
                .Include(fl => fl.User)
                .Include(fl => fl.Friend)
                .Where(fl => (fl.FriendID == id) && (fl.Status == true) || (fl.UserID == id) && (fl.Status == true))
                .ToListAsync();

            return await friends;
        }

    }
}

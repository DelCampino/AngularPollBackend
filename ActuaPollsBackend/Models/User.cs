using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class User
    {
        public long UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string Token { get; set; }
        public ICollection<PollUser> MyPolls { get; set; }
        public ICollection<FriendsList> RequestSend { get; set; }
        public ICollection<FriendsList> RequestGotten { get; set; }
    }
}

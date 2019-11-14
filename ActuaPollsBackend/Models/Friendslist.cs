using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class FriendsList
    {
        public long FriendsListID { get; set; }
        public long UserID { get; set; }

        public long FriendID { get; set; }

        public User Friend { get; set; }
        public User User { get; set; }
        public bool Status { get; set; } = false;
    }
}

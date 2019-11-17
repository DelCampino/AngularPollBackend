using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models.Dto
{
    public class UserInfo
    {
        public long UserID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public ICollection<FriendsList> RequestSend { get; set; }
        public ICollection<FriendsList> RequestGotten { get; set; }
    }
}

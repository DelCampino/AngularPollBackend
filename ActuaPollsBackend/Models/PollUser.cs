using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class PollUser
    {
        public long PollUserID { get; set; }
        public long UserID { get; set; }
        public long PollID { get; set; }

    }
}

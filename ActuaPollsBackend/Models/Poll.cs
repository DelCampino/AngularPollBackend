using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class Poll
    {
        public long PollID { get; set; }
        public string Name { get; set; }
        public ICollection<PollUser> Participants { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}

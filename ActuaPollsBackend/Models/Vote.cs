using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class Vote
    {
        public long VoteID { get; set; }
        public long AnswerID { get; set; }
        public long UserID { get; set; }
        public Answer Answer { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class Answer
    {
        public long AnswerID { get; set; }
        public string Text { get; set; }
        public long PollID { get; set; }
        public Poll Poll { get; set; }
        public ICollection<Vote> Votes { get; set; }
    }
}

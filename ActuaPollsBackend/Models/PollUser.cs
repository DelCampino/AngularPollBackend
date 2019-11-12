using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class PollUser
    {
        [Key, Column(Order = 1)]
        public long UserID { get; set; }
        [Key, Column(Order = 2)]
        public long PollID { get; set; }

        public Poll Poll { get; set; }
        public User User { get; set; }
    }
}

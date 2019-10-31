using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class Stem
    {
        public long StemID { get; set; }
        public long AntwoordID { get; set; }
        public long GebruikerID { get; set; }
    }
}

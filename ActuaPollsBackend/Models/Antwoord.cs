using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class Antwoord
    {
        public long AntwoordID { get; set; }
        public string Tekst { get; set; }
        public long PollID { get; set; }
    }
}

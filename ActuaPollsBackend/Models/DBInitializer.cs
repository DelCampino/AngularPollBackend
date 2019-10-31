using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ActuaPollsBackend.Models
{
    public class DBInitializer
    {
        public static void Initialize(PollsContext context)
        {
            context.Database.EnsureCreated();
            // Look for any verkiezingen.
            if (context.Polls.Any())
            {
                return;   // DB has been seeded
            }

            context.Users.AddRange(
                new User { Username = "test", Password = "test", Email = "test.test@thomasmore.be"});

            context.Polls.AddRange(
                new Poll { Naam = "test" });

            context.SaveChanges();
        }
    }
}

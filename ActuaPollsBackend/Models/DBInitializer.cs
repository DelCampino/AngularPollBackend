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
            
            if (context.Polls.Any())
            {
                return;   
            }

            context.Users.AddRange(
                new User { Username = "Sacha", Password = "test", Email = "sacha@thomasmore.be"});

            context.Users.AddRange(
                new User { Username = "Brecht", Password = "test", Email = "brecht@thomasmore.be" });

            context.Users.AddRange(
                new User { Username = "Boszie", Password = "test", Email = "boszie@thomasmore.be" });

            context.Users.AddRange(
                new User { Username = "Wannes", Password = "test", Email = "wannes@thomasmore.be" });

            context.Users.AddRange(
                new User { Username = "Nerissa", Password = "test", Email = "nerissa@thomasmore.be" });

            context.Users.AddRange(
                new User { Username = "Mathias", Password = "test", Email = "mathias@thomasmore.be" });


            context.SaveChanges();
        }
    }
}

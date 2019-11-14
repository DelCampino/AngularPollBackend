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
                new User { Username = "test", Password = "test", Email = "test.test@thomasmore.be"});

            context.Users.AddRange(
                new User { Username = "a", Password = "azerty", Email = "a@a.a" });

            context.Users.AddRange(
                new User
                {
                    Email = "b@b.b",
                    Username = "bbbbbb",
                    Password = "azerty",
                });

            context.Polls.AddRange(
                new Poll { Name = "test" });


            var testPoll = new Poll
            {
                Name = "testPoll"
            };
            

            context.Polls.Add(testPoll);
            context.SaveChanges();
        }
    }
}

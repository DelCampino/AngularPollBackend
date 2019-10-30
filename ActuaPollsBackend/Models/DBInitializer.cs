using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloAngularBackend.Models
{
    public class DBInitializer
    {
        public static void Initialize(MemberContext context)
        {
            context.Database.EnsureCreated();
            // Look for any verkiezingen.
            if (context.Members.Any())
            {
                return;   // DB has been seeded
            }
            context.Members.AddRange(
                new Member { Name = "Louis Louisen", Dob = new DateTime(1989, 11, 19), Address = "Langstraat100" },
                new Member { Name = "Roel Roelen", Dob = new DateTime(1989, 10, 19), Address = "Dorpsstraat50" });

            context.Users.AddRange(
                new User { Username = "test", Password = "test", FirstName = "Test", LastName = "Test", Email = "test.test@thomasmore.be"});

            context.SaveChanges();
        }
    }
}

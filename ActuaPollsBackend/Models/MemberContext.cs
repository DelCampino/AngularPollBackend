using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HelloAngularBackend.Models.Member;

namespace HelloAngularBackend.Models
{
    public class MemberContext : DbContext
    {
        public MemberContext(DbContextOptions<MemberContext> options) : base(options)
        {
        }
        public DbSet<Member> Members { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<User>().ToTable("User");


        }
    }
}

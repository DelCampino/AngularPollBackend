using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActuaPollsBackend.Models;

namespace ActuaPollsBackend.Models
{
    public class PollsContext : DbContext
    {
        public PollsContext(DbContextOptions<PollsContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("User");


        }
        public DbSet<ActuaPollsBackend.Models.Poll> Polls { get; set; }
        public DbSet<ActuaPollsBackend.Models.Antwoord> Antwoord { get; set; }
        public DbSet<ActuaPollsBackend.Models.Stem> Stem { get; set; }
        public DbSet<ActuaPollsBackend.Models.PollUser> PollUser { get; set; }
    }
}

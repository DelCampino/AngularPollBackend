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
        public DbSet<ActuaPollsBackend.Models.Poll> Polls { get; set; }
        public DbSet<ActuaPollsBackend.Models.Answer> Answers { get; set; }
        public DbSet<ActuaPollsBackend.Models.Vote> Votes { get; set; }
        public DbSet<ActuaPollsBackend.Models.PollUser> PollUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<PollUser>()
                .HasKey(x => new { x.UserID, x.PollID });

            modelBuilder.Entity<PollUser>()
                .HasOne(x => x.User)
                .WithMany(y => y.CreatedPolls)
                .HasForeignKey(y => y.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PollUser>()
                .HasOne(x => x.Poll)
                .WithMany(y => y.Participants)
                .HasForeignKey(y => y.PollID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Poll>()
                .HasMany(x => x.Answers)
                .WithOne(y => y.Poll)
                .HasForeignKey(y => y.PollID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasMany(x => x.Votes)
                .WithOne(y => y.Answer)
                .HasForeignKey(y => y.AnswerID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

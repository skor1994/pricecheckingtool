using Microsoft.EntityFrameworkCore;
using pricecheckingtoolapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pricecheckingtoolapi.Db
{
    public class DatabaseContext: DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Party> Partys { get; set; }
        public DbSet<PartyUser> PartyUser { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PartyUser>()
            .HasKey(pt => new { pt.userId, pt.partyId });

            modelBuilder.Entity<PartyUser>()
                .HasOne(pt => pt.user)
                .WithMany(p => p.partyUser)
                .HasForeignKey(pt => pt.userId);

            modelBuilder.Entity<PartyUser>()
                .HasOne(pt => pt.party)
                .WithMany(t => t.partyUser)
                .HasForeignKey(pt => pt.partyId);
        }
    }
}

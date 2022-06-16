using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using noam2.Model;

namespace noam2.Data
{
    public class noam2Context : DbContext
    {
        public noam2Context (DbContextOptions<noam2Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactExtended>().HasKey(c=> new {c.Id, c.MyUser});
        }

        public DbSet<noam2.Model.UserExtended>? UserExtended { get; set; }

        public DbSet<noam2.Model.ContactExtended>? ContactExtended { get; set; }

        public DbSet<noam2.Model.MessageExtanded>? MessageExtanded { get; set; }
    }
}

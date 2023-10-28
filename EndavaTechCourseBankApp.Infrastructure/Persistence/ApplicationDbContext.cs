using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndavaTechCourseBankApp.Domain.Models;


namespace EndavaTechCourseBankApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Wallet> wallets { get; set; }
        public DbSet<Currency> currencies { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(w  => w.Id);

            modelBuilder.Entity<Currency>().HasKey(w => w.Id);
            base.OnModelCreating(modelBuilder);
        }

    }
}

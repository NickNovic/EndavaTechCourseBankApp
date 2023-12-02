using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EndavaTechCourseBankApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EndavaTechCourseBankApp.Infrastructure.Новая_папка;
using Microsoft.Identity.Client;
using EndavaTechCourseBankApp.Domain.Enums;

namespace EndavaTechCourseBankApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Wallet> wallets { get; set; }
        public DbSet<Currency> currencies { get; set; }
        public DbSet<Transaction> transactions { get; set; } 
        public DbSet<Commision> commisions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>().HasKey(w  => w.Id);
            modelBuilder.Entity<Transaction>().HasKey(w => w.Id);
            modelBuilder.Entity<Commision>().HasKey(w => w.Id);

            modelBuilder.Entity<Currency>().HasKey(w => w.Id);
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            
            base.OnModelCreating(modelBuilder);
        }

    }
}

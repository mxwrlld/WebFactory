using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using _1._1.DAL.Configuration;
using _1._1.DAL.Model;

namespace _1._1.DAL
{
    class PersonalCustomerCardsContext: DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<PersonalCard> PersonalCards { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public PersonalCustomerCardsContext() : base() { }
        public PersonalCustomerCardsContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(new ConnectionStringManager().ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
            modelBuilder.ApplyConfiguration(new PersonalCardConfiguration());
            modelBuilder.ApplyConfiguration(new PurchaseConfiguration());
        }
    }
}

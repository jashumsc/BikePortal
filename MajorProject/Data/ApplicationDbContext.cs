

using MajorProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace MajorProject.Data
{
    public class ApplicationDbContext
       : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Bike> Bikes { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Rent> Rents { get; set; }

        public DbSet<Offer> Offers { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<BuyProduct> BuyProducts { get; set; }

        public DbSet<UpcomingEvent> UpcomingEvents { get; set; }


        public DbSet<RegisterEvent> EventParticipants { get; set; }

      
    }
}

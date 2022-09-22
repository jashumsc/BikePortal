﻿

using MajorProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    }
}
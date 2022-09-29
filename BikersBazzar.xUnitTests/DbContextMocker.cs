using MajorProject.Data;
using MajorProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BikersBazzar.xUnitTests
{
    public static class DbContextMocker
    {
        public static ApplicationDbContext GetApplicationDbContext(string dbName)
        {
           
            var serviceProvider = new ServiceCollection()
                                  .AddEntityFrameworkInMemoryDatabase()
                                  .BuildServiceProvider();

            // Create a new options instance telling the context
            // to use InMemory database with the new service provider created above
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                           .UseInMemoryDatabase(dbName)
                           .UseInternalServiceProvider(serviceProvider)
                           .Options;

            // Create the instance of the DbContext
            var dbContext = new ApplicationDbContext(options);

            // Add entities to the InMemory Database to seed the test data
            dbContext.SeedData();

            return dbContext;
        }


        internal static readonly Service[] TestData_Services
            = {
                new Service
                {
                    ServiceId = 0,
                    CustomerName ="Fist Customer",
                    CustomerPhone ="1234567890",
                    BikeNumber ="AA-00-AA-0000",
                    ServiceOn = "XXX",
                    //Status = true
                },
                new Service
                {
                     ServiceId = 0,
                    CustomerName ="second Customer",
                    CustomerPhone ="1234567891",
                    BikeNumber ="AA-00-AA-0000",
                    ServiceOn = "YYY",
                    //Status = true
                },
                new Service
                {
                     ServiceId = 0,
                    CustomerName ="Third Customer",
                    CustomerPhone ="1234567892",
                    BikeNumber ="AA-00-AA-0000",
                    ServiceOn = "ZZZ",
                    //Status = true
                },
            };


        /// <summary>
        ///     An extension Method for the DbContext object to Seed the Test Data.
        /// </summary>
        /// <param name="context">Application Db Context object.</param>
        private static void SeedData(this ApplicationDbContext context)
        {
            context.Services.AddRange(TestData_Services);

            // Commit the Changes to the database
            context.SaveChanges();
        }
    }
}

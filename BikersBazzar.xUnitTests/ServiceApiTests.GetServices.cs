using FluentAssertions;
using FluentAssertions.Common;
using MajorProject.Controllers;
using MajorProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BikersBazzar.xUnitTests
{
    public partial class ServiceApiTests
    {
        [Fact]
        public void GetServices_OkResult()
        {
            var dbName = nameof(ServiceApiTests.GetServices_OkResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new ServicesController(dbContext, logger);

            // ACT
            IActionResult actionResult = apiController.GetServices().Result;

            // ASSERT
            // --- Check if the ActionResult is of the type OkObjectResult
            Assert.IsType<OkObjectResult>(actionResult);

            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            int actualStatusCode = (actionResult as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetServices_CheckCorrectResult()
        {
            var dbName = nameof(ServiceApiTests.GetServices_OkResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            var dbContext = DbContextMocker.GetApplicationDbContext(dbName);
            var apiController = new ServicesController(dbContext, logger);
            
            ////
            IActionResult actionResult = apiController.GetServices().Result;

            ////
            Assert.IsType<OkObjectResult>(actionResult);
            
            ////
            var OkResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;

            ////
            Assert.IsAssignableFrom<List<Service>>(OkResult.Value);

            ////
            var servicesFromApi = OkResult.Value.Should().BeAssignableTo<List<Service>>().Subject;

            ////
            Assert.NotNull(servicesFromApi);


            ////
            Assert.Equal<int>(expected: DbContextMocker.TestData_Services.Length,
                                actual: servicesFromApi.Count);

            ////
            int ndx = 0;
            foreach(Service service in DbContextMocker.TestData_Services)
            {
                Assert.Equal<int>(expected: service.ServiceId,
                                    actual: servicesFromApi[ndx].ServiceId);

                Assert.Equal(expected: service.CustomerName,
                                   actual: servicesFromApi[ndx].CustomerName);
                
                Assert.Equal(expected: service.CustomerPhone,
                                   actual: servicesFromApi[ndx].CustomerPhone);

                Assert.Equal(expected: service.BikeNumber,
                                   actual: servicesFromApi[ndx].BikeNumber);

                Assert.Equal(expected: service.ServiceOn,
                                   actual: servicesFromApi[ndx].ServiceOn);

                _testOutputHelper.WriteLine($"Compared Row #{ndx} succesfully");
                ndx++;
            }

        }
    }
}

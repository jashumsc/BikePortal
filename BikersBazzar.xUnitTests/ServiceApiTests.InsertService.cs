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
        public void InsertService_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.InsertService_OkResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new ServicesController(dbContext, logger);
            Service categoryToAdd = new Service
            {
                ServiceId = 6,
                CustomerName = "Third Customer",
                CustomerPhone = "1234567892",
                BikeNumber = "AA-00-AA-0000",
                ServiceOn = "ZZZ",
                           // IF = null, then: INVALID!  CategoryName is REQUIRED
            };

            // ACT
            IActionResult actionResultPost = apiController.PostService(categoryToAdd).Result;

            // ASSERT - check if the IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultPost);

            // ASSERT - check if the Status Code is (HTTP 200) "Ok", (HTTP 201 "Created")
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultPost as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);

            // Extract the result from the IActionResult object.
            var postResult = actionResultPost.Should().BeOfType<OkObjectResult>().Subject;

            // ASSERT - if the result is a CreatedAtActionResult
            Assert.IsType<CreatedAtActionResult>(postResult.Value);

            // Extract the inserted Category object
            Service actualCategory = (postResult.Value as CreatedAtActionResult).Value
                                      .Should().BeAssignableTo<Service>().Subject;

            // ASSERT - if the inserted Category object is NOT NULL
            Assert.NotNull(actualCategory);

            Assert.Equal(categoryToAdd.ServiceId, actualCategory.ServiceId);
            Assert.Equal(categoryToAdd.CustomerName, actualCategory.CustomerName);
            Assert.Equal(categoryToAdd.CustomerPhone, actualCategory.CustomerPhone);
            Assert.Equal(categoryToAdd.BikeNumber, actualCategory.BikeNumber);
            Assert.Equal(categoryToAdd.ServiceOn, actualCategory.ServiceOn);

        }
    }
}

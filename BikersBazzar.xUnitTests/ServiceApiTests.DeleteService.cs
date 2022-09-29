using MajorProject.Controllers;
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
        public void DeleteService_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.DeleteService_NotFoundResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new ServicesController(dbContext, logger);
            int findServiceID = 900;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteService(findServiceID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultDelete as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCategory_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.DeleteCategory_BadRequestResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new ServicesController(dbContext, logger);
            int? findServiceID = null;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteService(findServiceID).Result;

            // ASSERT - check if the IActionResult is BadRequest 
            Assert.IsType<BadRequestResult>(actionResultDelete);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultDelete as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void DeleteCategory_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.DeleteCategory_BadRequestResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new ServicesController(dbContext, logger);
            int findServiceID = 1;

            // ACT
            IActionResult actionResultDelete = apiController.DeleteService(findServiceID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultDelete);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultDelete as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }
    }
}


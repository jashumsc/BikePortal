using FluentAssertions;
using MajorProject.Controllers;
using MajorProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BikersBazzar.xUnitTests
{
    /// <summary>
    /// <GetOkResult>
    ///     Recieves the Http response whether its providing Ok result
    /// </GetOkResult>
    /// <CheckCorrectResult>
    ///     Check the data ariived are correct
    /// </CheckCorrectResult>
    /// <NotFoundResult>
    ///     Test for data not availabe case
    /// </NotFoundResult>
    /// <BadRequestResult>
    ///     Recieves the Http response whether its providing Bad result
    /// </BadRequestResult>
    /// </summary>
    public partial class ServiceApiTests
    {
        [Fact]
        public void GetServiceById_NotFoundResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.GetServiceById_NotFoundResult);
            var logger = Mock.Of<ILogger<ServicesController>>();

            // using (var db = DbContextMocker.GetApplicationDbContext(dbName))
            // {
            // }           // db.Dispose(); implicitly called when you exit the USING Block

            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new ServicesController(dbContext, logger);
            int findCategoryID = 900;

            // ACT
            IActionResult actionResultGet = apiController.GetService(findCategoryID).Result;

            // ASSERT - check if the IActionResult is NotFound 
            Assert.IsType<NotFoundResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 404) "NotFound"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.NotFound;
            var actualStatusCode = (actionResultGet as NotFoundResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetServiceById_BadRequestResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.GetServiceById_BadRequestResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new ServicesController(dbContext, logger);
            int? findCategoryID = null;

            // ACT
            IActionResult actionResultGet = controller.GetService(findCategoryID).Result;

            // ASSERT - check if the IActionResult is BadRequest
            Assert.IsType<BadRequestResult>(actionResultGet);

            // ASSERT - check if the Status Code is (HTTP 400) "BadRequest"
            int expectedStatusCode = (int)System.Net.HttpStatusCode.BadRequest;
            var actualStatusCode = (actionResultGet as BadRequestResult).StatusCode;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }


        [Fact]
        public void GetCategoryById_OkResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.GetCategoryById_OkResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new ServicesController(dbContext, logger);
            int findCategoryID = 2;

            // ACT
            IActionResult actionResultGet = controller.GetService(findCategoryID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if Status Code is HTTP 200 (Ok)
            int expectedStatusCode = (int)System.Net.HttpStatusCode.OK;
            var actualStatusCode = (actionResultGet as OkObjectResult).StatusCode.Value;
            Assert.Equal<int>(expectedStatusCode, actualStatusCode);
        }

        [Fact]
        public void GetServiceById_CorrectResult()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.GetServiceById_CorrectResult);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var controller = new ServicesController(dbContext, logger);
            int findServiceID = 2;
            Service expectedService = DbContextMocker.TestData_Services
                                        .SingleOrDefault(s => s.ServiceId == findServiceID);

            // ACT
            IActionResult actionResultGet = controller.GetService(findServiceID).Result;

            // ASSERT - if IActionResult is Ok
            Assert.IsType<OkObjectResult>(actionResultGet);

            // ASSERT - if IActionResult (i.e., OkObjectResult) contains an object of the type Category
            OkObjectResult okResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;
            Assert.IsType<Service>(okResult.Value);

            // Extract the category object from the result.
            Service actualService = okResult.Value.Should().BeAssignableTo<Service>().Subject;
            _testOutputHelper.WriteLine($"Found: CategoryID == {actualService.ServiceId}");

            // ASSERT - if category is NOT NULL
            Assert.NotNull(actualService);

            // ASSERT - if the CategoryId is containing the expected data.
            Assert.Equal<int>(expected: expectedService.ServiceId,
                              actual: actualService.ServiceId);

            // ASSERT - if the CateogoryName is correct 
            Assert.Equal(expected: expectedService.CustomerName,
                         actual: actualService.CustomerName);
        }

    }
}

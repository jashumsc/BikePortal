using FluentAssertions;
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
    /// <remarks>
    ///     Bad update data scenarios:
    ///     - Name is NULL
    ///     - Name is EMPTY STRING
    ///     - Name contains more characters than what is allowed
    ///     - NULL Service object
    ///     - ID not matching with the ID of the row identified to be updated.
    ///     - ID replaced with a negative value
    ///     - Replacing the object retrieved before the update, with a completely new object
    ///     - Since the HTTP PUT receives a Nullable INT as first parameter, pass a NULL value
    ///
    ///     LIMITATIONS OF WORKING WITH InMemory Database
    ///     - Relationships between Tables are not supported.
    ///     - EF Core DataAnnotation Validations are not supported.
    /// </remarks>
    public partial class ServiceApiTests
    {
       

        [Fact]
        public async void UpdateService_OkResult02()
        {
            // ARRANGE
            var dbName = nameof(ServiceApiTests.UpdateService_OkResult02);
            var logger = Mock.Of<ILogger<ServicesController>>();
            using var dbContext = DbContextMocker.GetApplicationDbContext(dbName);      // Disposable!
            var apiController = new ServicesController(dbContext, logger);
            int editServiceId = 2;
            Service originalService;
            string changedServiceName = "--- CHANGED Service type";

            // ACT #1: Get the Record to Edit.

            // (a) Get the Category to edit (to ensure that the row exists before editing it)
            IActionResult actionResultGet = await apiController.GetService(editServiceId);

            // (b) Check if HTTP 200 "Ok" 
            OkObjectResult OkResult = actionResultGet.Should().BeOfType<OkObjectResult>().Subject;

            // (c) Extract the Category Object from the OkObjectResult
            originalService = OkResult.Value.Should().BeAssignableTo<Service>().Subject;

            // (d) Check if the data to be edited was received from the API
            Assert.NotNull(originalService);

            _testOutputHelper.WriteLine("Retrieved the Data from the API.");

            // ACT #2: Now, try to update the data
            // SOLUTION: The following lines would work!
            //           Reason, we are modifying the data originally received.
            //           And then, calling the PUT operation.
            //           So, the API is able to find the ChangeTracking data associated to the object.

            // (a) Change the data of the object that was received from the API.
            originalService.ServiceOn = changedServiceName;

            // (b) Call the HTTP PUT API to update the changes (with the updated object)
            IActionResult actionResultPutAttempt2 = await apiController.PutService(editServiceId, originalService);

            // ASSERT - if the update was successfull.
            Assert.IsType<NoContentResult>(actionResultPutAttempt2);

            _testOutputHelper.WriteLine("Updated the changes back to the API - using the object received");
        }

    }
}

using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Controllers;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System.Collections.Generic;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ControllerTests
{
    [Trait("Api", nameof(ActivationsControllerTests))]
    public class ActivationsControllerTests
    {
        protected ActivationsController ActivationsController { get; }
        protected Mock<IActivationsService> ActivationsServiceMock { get; }

        public ActivationsControllerTests()
        {
            ActivationsServiceMock = new Mock<IActivationsService>();
            ActivationsController = new ActivationsController(ActivationsServiceMock.Object);
        }

        #region Get Activations

        [Fact]
        public async void GetActivations_ReturnsOkObjectResult_WhenServiceReturnsActivations()
        {
            // Arrange
            var expectedActivations = TestData.Activations.ContractActivations;
            ActivationsServiceMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedActivations);

            // Act
            var result = await ActivationsController.GetManyAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedActivations, okResult.Value);
        }

        [Fact]
        public async void GetActivations_ReturnsOkObjectResult_WhenServiceReturnsEmptyList()
        {
            // Arrange
            var expectedActivations = new List<Activation>();
            ActivationsServiceMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedActivations);

            // Act
            var result = await ActivationsController.GetManyAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedActivations, okResult.Value);
        }

        #endregion

        #region Get Activation

        [Fact]
        public async void GetActivation_ReturnsOkObjectResult_WhenServiceReturnsActivation()
        {
            // Arrange
            var expectedActivation = TestData.Activations.ContractActivation;
            ActivationsServiceMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedActivation);

            // Act
            var result = await ActivationsController.GetOneByIdAsync("123021");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedActivation, okResult.Value);
        }

        [Fact]
        public async void GetActivation_ReturnsNotFoundResult_WhenServiceReturnsNull()
        {
            // Arrange
            ActivationsServiceMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ActivationsController.GetOneByIdAsync("123021");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Post Activation

        [Fact]
        public async void PostActivation_ReturnsCreatedAtActionResult_WhenServiceReturnsActivation()
        {
            // Arrange
            var activationPostDto = TestData.Activations.ContractActivationPostDto;
            var expectedActivation = TestData.Activations.ContractActivationPostDtoResponseMock;
            ActivationsServiceMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Activation>()))
                .ReturnsAsync(expectedActivation);

            // Act
            var result = await ActivationsController.PostOneAsync(activationPostDto);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.True(createdResult.StatusCode == 201);
        }

        [Fact]
        public async void PostActivation_ReturnsBadRequestObjectResult_WhenServiceReturnsNull()
        {
            // Arrange
            var activationPostDto = TestData.Activations.ContractActivationPostDto;
            ActivationsServiceMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Activation>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ActivationsController.PostOneAsync(activationPostDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void PostActivation_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var activationPostDto = TestData.Activations.ContractActivationPostDto;
            ActivationsController.ModelState.AddModelError("Mock", "Error");

            // Act
            var result = await ActivationsController.PostOneAsync(activationPostDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region Delete Activation

        [Fact]
        public async void DeleteActivation_ReturnsNoContentResult_WhenServiceReturnsDeletedActivation()
        {
            // Arrange
            var deletedActivation = TestData.Activations.ContractActivation;
            ActivationsServiceMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedActivation);

            // Act
            var result = await ActivationsController.DeleteOneByIdAsync("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteActivation_ReturnsNoContentResult_WhenServiceReturnsNull()
        {
            // Arrange
            ActivationsServiceMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ActivationsController.DeleteOneByIdAsync("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        #endregion
    }
}

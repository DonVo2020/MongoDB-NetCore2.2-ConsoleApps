using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Controllers;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System.Collections.Generic;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ControllerTests
{
    [Trait("Api", nameof(MusclesControllerTests))]
    public class MusclesControllerTests
    {
        protected MusclesController MusclesController { get; }
        protected Mock<IMusclesService> MusclesServiceMock { get; }

        public MusclesControllerTests()
        {
            MusclesServiceMock = new Mock<IMusclesService>();
            MusclesController = new MusclesController(MusclesServiceMock.Object);
        }

        #region Task<IActionResult> GetManyAsync()

        [Fact]
        public async void GetManyAsync_ReturnsOkObjectResult_WhenServiceReturnsMuscles()
        {
            // Arrange
            var expectedMuscles = TestData.Muscles.ContractMuscles;
            MusclesServiceMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedMuscles);

            // Act
            var result = await MusclesController.GetManyAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedMuscles, okResult.Value);
        }

        [Fact]
        public async void GetManyAsync_ReturnsOkObjectResult_WhenServiceReturnsEmptyList()
        {
            // Arrange
            var expectedMuscles = new List<Muscle>();
            MusclesServiceMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedMuscles);

            // Act
            var result = await MusclesController.GetManyAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedMuscles, okResult.Value);
        }

        #endregion

        #region Task<IActionResult> GetOneByIdAsync(string id)

        [Fact]
        public async void GetOneByIdAsync_ReturnsOkObjectResult_WhenServiceReturnsMuscle()
        {
            // Arrange
            var expectedMuscle = TestData.Muscles.ContractMuscle;
            MusclesServiceMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMuscle);

            // Act
            var result = await MusclesController.GetOneByIdAsync("123021");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedMuscle, okResult.Value);
        }

        [Fact]
        public async void GetOneByIdAsync_ReturnsNotFoundResult_WhenServiceReturnsNull()
        {
            // Arrange
            MusclesServiceMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Muscle)null);

            // Act
            var result = await MusclesController.GetOneByIdAsync("123021");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Task<IActionResult> PostOneAsync([FromBody] PostMuscleDto MuscleDto)

        [Fact]
        public async void PostOneAsync_ReturnsCreatedAtActionResult_WhenServiceReturnsMuscle()
        {
            // Arrange
            var musclePostDto = TestData.Muscles.ContractMusclePostDto;
            var expectedMuscle = TestData.Muscles.MuscleWithoutId;
            expectedMuscle.GroupId = "1234";
            MusclesServiceMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Muscle>()))
                .ReturnsAsync(expectedMuscle);

            // Act
            var result = await MusclesController.PostOneAsync(musclePostDto);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.True(createdResult.StatusCode == 201);
        }

        [Fact]
        public async void PostOneAsync_ReturnsBadRequestObjectResult_WhenServiceReturnsNull()
        {
            // Arrange
            var musclePostDto = TestData.Muscles.ContractMusclePostDto;
            MusclesServiceMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Muscle>()))
                .ReturnsAsync((Muscle)null);

            // Act
            var result = await MusclesController.PostOneAsync(musclePostDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void PostOneAsync_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var musclePostDto = TestData.Muscles.ContractMusclePostDto;
            MusclesController.ModelState.AddModelError("Mock", "Error");

            // Act
            var result = await MusclesController.PostOneAsync(musclePostDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region Task<IActionResult> DeleteOneByIdAsync(string id)

        [Fact]
        public async void DeleteOneByIdAsync_ReturnsNoContentResult_WhenServiceReturnsDeletedMuscle()
        {
            // Arrange
            var deletedMuscle = TestData.Muscles.ContractMuscle;
            MusclesServiceMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedMuscle);

            // Act
            var result = await MusclesController.DeleteOneByIdAsync("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteOneByIdAsync_ReturnsNoContentResult_WhenServiceReturnsNull()
        {
            // Arrange
            MusclesServiceMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Muscle)null);

            // Act
            var result = await MusclesController.DeleteOneByIdAsync("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        #endregion
    }
}

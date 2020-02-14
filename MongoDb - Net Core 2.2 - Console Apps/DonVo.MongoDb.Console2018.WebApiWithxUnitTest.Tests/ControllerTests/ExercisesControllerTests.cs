using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Controllers;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;

using Microsoft.AspNetCore.Mvc;

using Moq;

using System.Collections.Generic;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ControllerTests
{
    [Trait("Api", nameof(ExercisesControllerTests))]
    public class ExercisesControllerTests
    {
        protected ExercisesController ExercisesController { get; }
        protected Mock<IExercisesService> ExercisesServiceMock { get; }

        public ExercisesControllerTests()
        {
            ExercisesServiceMock = new Mock<IExercisesService>();
            ExercisesController = new ExercisesController(ExercisesServiceMock.Object);
        }

        #region Get Exercises

        [Fact]
        public async void GetExercises_ReturnsOkObjectResult_WhenServiceReturnsExercises()
        {
            // Arrange
            var expectedExercises = TestData.Exercises.ContractExercises;
            ExercisesServiceMock
                .Setup(x => x.FindExercises())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesController.GetExercises();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedExercises, okResult.Value);
        }

        [Fact]
        public async void GetExercises_ReturnsOkObjectResult_WhenServiceReturnsEmptyList()
        {
            // Arrange
            var expectedExercises = new List<Exercise>();
            ExercisesServiceMock
                .Setup(x => x.FindExercises())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesController.GetExercises();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedExercises, okResult.Value);
        }

        #endregion

        #region Get Exercise

        [Fact]
        public async void GetExercise_ReturnsOkObjectResult_WhenServiceReturnsExercise()
        {
            // Arrange
            var expectedExercise = TestData.Exercises.ContractExercise;
            ExercisesServiceMock
                .Setup(x => x.FindExercise(It.IsAny<string>()))
                .ReturnsAsync(expectedExercise);

            // Act
            var result = await ExercisesController.GetExercise("123021");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedExercise, okResult.Value);
        }

        [Fact]
        public async void GetExercise_ReturnsNotFoundResult_WhenServiceReturnsNull()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.FindExercise(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesController.GetExercise("123021");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Post Exercise

        [Fact]
        public async void PostExercise_ReturnsCreatedAtActionResult_WhenServiceReturnsExercise()
        {
            // Arrange
            var exercisePostDto = TestData.Exercises.ContractExercisePostDto;
            var expectedExercise = TestData.Exercises.ContractExercisePostDtoResponseMock;
            ExercisesServiceMock
                .Setup(x => x.CreateExercise(It.IsAny<Exercise>()))
                .ReturnsAsync(expectedExercise);

            // Act
            var result = await ExercisesController.PostExercise(exercisePostDto);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.True(createdResult.StatusCode == 201);
        }

        [Fact]
        public async void PostExercise_ReturnsBadRequestObjectResult_WhenServiceReturnsNull()
        {
            // Arrange
            var exercisePostDto = TestData.Exercises.ContractExercisePostDto;
            ExercisesServiceMock
                .Setup(x => x.CreateExercise(It.IsAny<Exercise>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesController.PostExercise(exercisePostDto);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async void PostExercise_ReturnsBadRequestObjectResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var exercisePostDto = TestData.Exercises.ContractExercisePostDto;
            ExercisesController.ModelState.AddModelError("Mock", "Error");

            // Act
            var result = await ExercisesController.PostExercise(exercisePostDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion

        #region Delete Exercise

        [Fact]
        public async void DeleteExercise_ReturnsNoContentResult_WhenServiceReturnsDeletedExercise()
        {
            // Arrange
            var deletedExercise = TestData.Exercises.ContractExercise;
            ExercisesServiceMock
                .Setup(x => x.DeleteExercise(It.IsAny<string>()))
                .ReturnsAsync(deletedExercise);

            // Act
            var result = await ExercisesController.DeleteExercise("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteExercise_ReturnsNoContentResult_WhenServiceReturnsNull()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.DeleteExercise(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesController.DeleteExercise("123021");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        #endregion

        #region Delete Activation From Exercise

        [Fact]
        public async void DeleteActivationFromExercise_ReturnsNoContentResult_WhenServiceReturnsDeletedActivation()
        {
            // Arrange
            var deletedActivation = TestData.Activations.ContractActivation;
            ExercisesServiceMock
                .Setup(x => x.DeleteActivation(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(deletedActivation);

            // Act
            var result = await ExercisesController.DeleteActivationFromExercise("ExerciseId", "ActivationId");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteActivationFromExercise_ReturnsNoContentResult_WhenServiceReturnsEmptyActivation()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.DeleteActivation(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new Activation());

            // Act
            var result = await ExercisesController.DeleteActivationFromExercise("ExerciseId", "ActivationId");

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteActivationFromExercise_ReturnsNotFoundResult_WhenServiceReturnsNull()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.DeleteActivation(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ExercisesController.DeleteActivationFromExercise("ExerciseId", "ActivationId");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Get Activation for an Exercise

        [Fact]
        public async void GetActivationForExercise_ReturnsOkObjectResult_WhenServiceReturnsActivation()
        {
            // Arrange
            var expectedActivation = TestData.Activations.ContractActivation;
            ExercisesServiceMock
                .Setup(x => x.FindActivation(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(expectedActivation);

            // Act
            var result = await ExercisesController.GetActivationForExercise("ExerciseId", "ActivationId");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedActivation, okResult.Value);
        }

        [Fact]
        public async void GetActivationForExercise_ReturnNotFoundResult_WhenServiceReturnsNull()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.FindActivation(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ExercisesController.GetActivationForExercise("ExerciseId", "ActivationId");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Get Activations for an Exercise

        [Fact]
        public async void GetActivationsForExercise_ReturnsOkObjectResult_WhenServiceReturnsActivations()
        {
            // Arrange
            var expectedActivations = TestData.Activations.ContractActivations;
            ExercisesServiceMock
                .Setup(x => x.FindActivations(It.IsAny<string>()))
                .ReturnsAsync(expectedActivations);

            // Act
            var result = await ExercisesController.GetActivationsForExercise("ExerciseId");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedActivations, okResult.Value);
        }

        [Fact]
        public async void GetActivationsForExercise_ReturnsEmptyOkObjectResult_WhenServiceReturnsEmptyResult()
        {
            // Arrange
            var expectedActivations = new List<Activation>();
            ExercisesServiceMock
                .Setup(x => x.FindActivations(It.IsAny<string>()))
                .ReturnsAsync(expectedActivations);

            // Act
            var result = await ExercisesController.GetActivationsForExercise("ExerciseId");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Same(expectedActivations, okResult.Value);
        }

        [Fact]
        public async void GetActivationsForExercise_ReturnNotFoundResult_WhenServiceReturnsNull()
        {
            // Arrange
            ExercisesServiceMock
                .Setup(x => x.FindActivations(It.IsAny<string>()))
                .ReturnsAsync((IEnumerable<Activation>)null);

            // Act
            var result = await ExercisesController.GetActivationsForExercise("ExerciseId");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region Post an Activation to an Exercise

        [Fact]
        public async void PostActivationToExercise_ReturnsCreatedAtResult_WhenServiceReturnsCreatedActivation()
        {
            // Arrange
            var activationPostDto = TestData.Activations.ContractActivationPostDto;
            var expectedActivation = TestData.Activations.ContractActivationPostDtoResponseMock;
            ExercisesServiceMock
                .Setup(x => x.CreateActivation(It.IsAny<string>(), It.IsAny<Activation>()))
                .ReturnsAsync(expectedActivation);

            // Act
            var result = await ExercisesController.PostActivationToExercise("exerciseId", activationPostDto);

            // Assert
            var createdResult = Assert.IsType<ObjectResult>(result);
            Assert.True(createdResult.StatusCode == 201);

        }

        [Fact]
        public async void PostActivationToExercise_ReturnsNotFoundObject_WhenServiceReturnsNull()
        {
            // Arrange
            var activationPostDto = TestData.Activations.ContractActivationPostDto;
            ExercisesServiceMock
                .Setup(x => x.CreateActivation(It.IsAny<string>(), It.IsAny<Activation>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ExercisesController.PostActivationToExercise("exerciseId", activationPostDto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void PostActivationToExercise_ReturnsBadRequestObject_WhenModelStateIsInvalid()
        {
            // Arrange
            var activationPostDto = TestData.Activations.ContractActivationPostDto;
            ExercisesController.ModelState.AddModelError("Mock", "Error");

            // Act
            var result = await ExercisesController.PostActivationToExercise("exerciseId", activationPostDto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        #endregion
    }
}

using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;

using Moq;

using System.Collections.Generic;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ServicesTests
{
    [Trait("Service", nameof(ExercisesServiceTests))]
    public class ExercisesServiceTests
    {
        protected ExercisesService ExercisesService { get; }
        protected Mock<IActivationsRepository> ActivationsRepositoryMock { get; }
        protected Mock<IExercisesRepository> ExercisesRepositoryMock { get; }

        public ExercisesServiceTests()
        {
            ActivationsRepositoryMock = new Mock<IActivationsRepository>();
            ExercisesRepositoryMock = new Mock<IExercisesRepository>();
            ExercisesService = new ExercisesService(ActivationsRepositoryMock.Object, ExercisesRepositoryMock.Object);
        }

        #region Find Exercises

        [Fact]
        public async void FindExercises_ReturnsExercises_WhenRepositoryReturnsExercises()
        {
            // Arrange
            var expectedExercises = TestData.Exercises.ContractExercises;
            ExercisesRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesService.FindExercises();

            // Assert
            Assert.Equal(expectedExercises, result);
        }

        [Fact]
        public async void FindExercises_ReturnsEmptyArray_WhenRepositoryReturnsEmptyArray()
        {
            // Arrange
            var expectedExercises = new List<Exercise>();
            ExercisesRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedExercises);

            // Act
            var result = await ExercisesService.FindExercises();

            // Assert
            Assert.Equal(expectedExercises, result);
        }

        #endregion

        #region Find Exercise by Id

        [Fact]
        public async void FindExercise_ReturnsExercise_WhenRepositoryReturnsExercise()
        {
            // Arrange
            var expectedExercise = TestData.Exercises.ContractExercise;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedExercise);

            // Act
            var result = await ExercisesService.FindExercise("123021");

            // Assert
            Assert.Equal(expectedExercise, result);
        }

        [Fact]
        public async void FindExercise_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.FindExercise("123021");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Find Single Activation

        [Fact]
        public async void FindActivation_ReturnsActivation_WhenRepositoryReturnsActivation()
        {
            // Arrange
            var expectedExercise = TestData.Exercises.ContractExercise;
            var expectedActivation = TestData.Activations.ContractActivation;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedExercise);
            ActivationsRepositoryMock
                .Setup((x => x.ReadOneAsync(It.IsAny<string>())))
                .ReturnsAsync(expectedActivation);

            // Act
            var result = await ExercisesService.FindActivation("ExerciseId", "ActivationId");

            // Assert
            Assert.Equal(expectedActivation, result);
        }

        [Fact]
        public async void FindActivation_ReturnsNull_WhenRepositoryReturnsNullExercise()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);


            // Act
            var result = await ExercisesService.FindActivation("ExerciseId", "ActivationId");

            // Assert
            ActivationsRepositoryMock.Verify(x => x.ReadOneAsync(It.IsAny<string>()), Times.Never);
            Assert.Null(result);
        }

        [Fact]
        public async void FindActivation_ReturnsNull_WhenRepositoryReturnsNullActivation()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(new Exercise());
            ActivationsRepositoryMock
                .Setup((x => x.ReadOneAsync(It.IsAny<string>())))
                .ReturnsAsync((Activation)null);


            // Act
            var result = await ExercisesService.FindActivation("ExerciseId", "ActivationId");

            // Assert
            ActivationsRepositoryMock.Verify(x => x.ReadOneAsync(It.IsAny<string>()), Times.Once());
            Assert.Null(result);
        }

        #endregion

        #region Find Many Activations

        [Fact]
        public async void FindActivations_ReturnsActivation_WhenRepositoryReturnsActivation()
        {
            // Arrange
            var expectedExercise = TestData.Exercises.ContractExercise;
            var expectedActivations = TestData.Activations.ContractActivations;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedExercise);
            ActivationsRepositoryMock
                .Setup((x => x.ReadManyAsync(It.IsAny<string>())))
                .ReturnsAsync(expectedActivations);

            // Act
            var result = await ExercisesService.FindActivations("ExerciseId");

            // Assert
            Assert.Equal(expectedActivations, result);
        }

        [Fact]
        public async void FindActivations_ReturnsNull_WhenRepositoryReturnsNullExercise()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.FindActivations("ExerciseId");

            // Assert
            //ActivationsRepositoryMock.Verify(x => x.ReadManyAsync(It.IsAny<string>()), Times.Never);
            Assert.Null(result);
        }

        [Fact]
        public async void FindActivations_ReturnsEmptyEnumerable_WhenRepositoryReturnsEmptyActivationsEnumerable()
        {
            // Arrange
            var expectedActivations = new List<Activation>();
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(new Exercise());
            ActivationsRepositoryMock
                .Setup((x => x.ReadManyAsync(It.IsAny<string>())))
                .ReturnsAsync(expectedActivations);


            // Act
            var result = await ExercisesService.FindActivations("ExerciseId");

            // Assert
            Assert.Equal(expectedActivations, result);
        }

        #endregion

        #region Create Exercise

        [Fact]
        public async void CreateExercise_ReturnsCreatedExercise_WhenServiceReturnsExercise()
        {
            // Arrange
            var exerciseToCreate = TestData.Exercises.ExerciseWithoutId;
            var createdExercise = TestData.Exercises.ContractExercisePostDtoResponseMock;
            ExercisesRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Exercise>()))
                .ReturnsAsync(createdExercise);

            // Act
            var result = await ExercisesService.CreateExercise(exerciseToCreate);

            // Assert
            Assert.Equal(createdExercise, result);
        }

        [Fact]
        public async void CreateExercise_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var exerciseToCreate = TestData.Exercises.ExerciseWithoutId;
            ExercisesRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Exercise>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.CreateExercise(exerciseToCreate);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Create Activation

        [Fact]
        public async void CreateActivation_ReturnsActivation_WhenRepositoryReturnsCreatedActivation()
        {
            // Arrange
            var activationToCreate = TestData.Activations.ActivationWithoutId;
            var createdActivation = TestData.Activations.ContractActivationPostDtoResponseMock;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(new Exercise());
            ActivationsRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Activation>()))
                .ReturnsAsync(createdActivation);

            // Act
            var result = await ExercisesService.CreateActivation("ExerciseId", activationToCreate);

            // Assert
            Assert.Equal(createdActivation, result);
        }

        [Fact]
        public async void CreateActivation_ReturnsNullActivation_WhenRepositoryReturnsNullExercise()
        {
            // Arrange
            var activationToCreate = TestData.Activations.ActivationWithoutId;
            var createdActivation = TestData.Activations.ContractActivationPostDtoResponseMock;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);
            ActivationsRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Activation>()))
                .ReturnsAsync(createdActivation);

            // Act
            var result = await ExercisesService.CreateActivation("ExerciseId", activationToCreate);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void CreateActivation_ReturnsNullActivation_WhenRepositoryReturnsNullActivation()
        {
            // Arrange
            var activationToCreate = TestData.Activations.ActivationWithoutId;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(new Exercise());
            ActivationsRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Activation>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ExercisesService.CreateActivation("ExerciseId", activationToCreate);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Delete Exercise

        [Fact]
        public async void DeleteExercise_ReturnsExercise_WhenRepositoryReturnsExercise()
        {
            // Arrange
            var deletedExercise = TestData.Exercises.ContractExercise;
            ExercisesRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedExercise);

            // Act
            var result = await ExercisesService.DeleteExercise("123021");

            // Assert
            Assert.Equal(deletedExercise, result);
        }

        [Fact]
        public async void DeleteExercise_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.DeleteExercise("123021");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Delete Activation

        [Fact]
        public async void DeleteActivation_ReturnsDeletedActivation_WhenRepositoryReturnsActivation()
        {
            // Arrange
            var deletedActivation = TestData.Activations.ContractActivation;
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(new Exercise());
            ActivationsRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedActivation);

            // Act
            var result = await ExercisesService.DeleteActivation("ExerciseId", "ActivationId");

            // Assert
            Assert.Equal(deletedActivation, result);
        }

        [Fact]
        public async void DeleteActivation_ReturnsEmptyActivation_WhenRepositoryRetrunsNullActivation()
        {
            // Arrange
            var emptyActivation = new Activation();
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(new Exercise());
            ActivationsRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ExercisesService.DeleteActivation("ExerciseId", "ActivationId");

            // Assert
            Assert.Equal(emptyActivation, result);
        }

        [Fact]
        public async void DeleteActivation_ReturnsNullActivation_WhenRepositoryReturnsNullExercise()
        {
            // Arrange
            ExercisesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Exercise)null);

            // Act
            var result = await ExercisesService.DeleteActivation("ExerciseId", "ActivationId");

            // Assert
            ActivationsRepositoryMock.Verify(x => x.DeleteOneAsync(It.IsAny<string>()), Times.Never);
            Assert.Null(result);
        }

        #endregion
    }
}

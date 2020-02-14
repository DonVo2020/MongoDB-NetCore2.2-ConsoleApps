using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;

using Moq;

using System.Collections.Generic;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ServicesTests
{
    [Trait("Service", nameof(MusclesServiceTests))]
    public class MusclesServiceTests
    {
        protected MusclesService MusclesService { get; }
        protected Mock<IMusclesRepository> MusclesRepositoryMock { get; }

        public MusclesServiceTests()
        {
            MusclesRepositoryMock = new Mock<IMusclesRepository>();
            MusclesService = new MusclesService(MusclesRepositoryMock.Object);
        }

        #region Task<IEnumerable<Muscle>> ReadAllAsync()

        [Fact]
        public async void ReadAllAsync_ReturnsMuscles_WhenRepositoryReturnsMuscles()
        {
            // Arrange
            var expectedMuscles = TestData.Muscles.ContractMuscles;
            MusclesRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedMuscles);

            // Act
            var result = await MusclesService.ReadAllAsync();

            // Assert
            Assert.Same(expectedMuscles, result);
        }

        [Fact]
        public async void ReadAllAsync_ReturnsEmptyArray_WhenRepositoryReturnsEmptyArray()
        {
            // Arrange
            var expectedMuscles = new List<Muscle>();
            MusclesRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedMuscles);

            // Act
            var result = await MusclesService.ReadAllAsync();

            // Assert
            Assert.Same(expectedMuscles, result);
        }

        #endregion

        #region Task<Muscle> ReadOneAsync(string id)

        [Fact]
        public async void ReadOneAsync_ReturnsMuscle_WhenRepositoryReturnsMuscle()
        {
            // Arrange
            var expectedMuscle = TestData.Muscles.ContractMuscle;
            MusclesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMuscle);

            // Act
            var result = await MusclesService.ReadOneAsync("123021");

            // Assert
            Assert.Same(expectedMuscle, result);
        }

        [Fact]
        public async void ReadOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            MusclesRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Muscle)null);

            // Act
            var result = await MusclesService.ReadOneAsync("123021");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Task<Muscle> CreateOneAsync(Muscle muscleToCreate)

        [Fact]
        public async void CreateOneAsync_ReturnsCreatedMuscle_WhenServiceReturnsMuscle()
        {
            // Arrange
            var muscleToCreate = TestData.Muscles.MuscleWithoutId;
            var createdMuscle = TestData.Muscles.MuscleWithoutId;
            createdMuscle.Id = "1234";
            MusclesRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Muscle>()))
                .ReturnsAsync(createdMuscle);

            // Act
            var result = await MusclesService.CreateOneAsync(muscleToCreate);

            // Assert
            Assert.Same(createdMuscle, result);
        }

        [Fact]
        public async void CreateOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var muscleToCreate = TestData.Muscles.MuscleWithoutId;
            MusclesRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Muscle>()))
                .ReturnsAsync((Muscle)null);

            // Act
            var result = await MusclesService.CreateOneAsync(muscleToCreate);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Task<Muscle> DeleteOneAsync(string id)

        [Fact]
        public async void DeleteOneAsync_ReturnsMuscle_WhenRepositoryReturnsMuscle()
        {
            // Arrange
            var deletedMuscle = TestData.Muscles.ContractMuscle;
            MusclesRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedMuscle);

            // Act
            var result = await MusclesService.DeleteOneAsync("123021");

            // Assert
            Assert.Same(deletedMuscle, result);
        }

        [Fact]
        public async void DeleteOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            MusclesRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Muscle)null);

            // Act
            var result = await MusclesService.DeleteOneAsync("123021");

            // Assert
            Assert.Null(result);
        }

        #endregion
    }
}

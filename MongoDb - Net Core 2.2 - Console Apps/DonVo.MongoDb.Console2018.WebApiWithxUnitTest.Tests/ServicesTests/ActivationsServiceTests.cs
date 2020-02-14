using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Interfaces;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Services;

using Moq;

using System.Collections.Generic;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.ServicesTests
{
    [Trait("Service", nameof(ActivationsServiceTests))]
    public class ActivationsServiceTests
    {
        protected ActivationsService ActivationsService { get; }
        protected Mock<IActivationsRepository> ActivationsRepositoryMock { get; }

        public ActivationsServiceTests()
        {
            ActivationsRepositoryMock = new Mock<IActivationsRepository>();
            ActivationsService = new ActivationsService(ActivationsRepositoryMock.Object);
        }

        #region Task<IEnumerable<Activation>> ReadAllAsync()

        [Fact]
        public async void ReadAllAsync_ReturnsActivations_WhenRepositoryReturnsActivations()
        {
            // Arrange
            var expectedActivations = TestData.Activations.ContractActivations;
            ActivationsRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedActivations);

            // Act
            var result = await ActivationsService.ReadAllAsync();

            // Assert
            Assert.Same(expectedActivations, result);
        }

        [Fact]
        public async void ReadAllAsync_ReturnsEmptyArray_WhenRepositoryReturnsEmptyArray()
        {
            // Arrange
            var expectedActivations = new List<Activation>();
            ActivationsRepositoryMock
                .Setup(x => x.ReadAllAsync())
                .ReturnsAsync(expectedActivations);

            // Act
            var result = await ActivationsService.ReadAllAsync();

            // Assert
            Assert.Same(expectedActivations, result);
        }

        #endregion

        #region Task<Activation> ReadOneAsync(string id)

        [Fact]
        public async void ReadOneAsync_ReturnsActivation_WhenRepositoryReturnsActivation()
        {
            // Arrange
            var expectedActivation = TestData.Activations.ContractActivation;
            ActivationsRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedActivation);

            // Act
            var result = await ActivationsService.ReadOneAsync("123021");

            // Assert
            Assert.Same(expectedActivation, result);
        }

        [Fact]
        public async void ReadOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            ActivationsRepositoryMock
                .Setup(x => x.ReadOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ActivationsService.ReadOneAsync("123021");

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Task<Activation> CreateOneAsync(Activation muscleToCreate)

        [Fact]
        public async void CreateOneAsync_ReturnsCreatedActivation_WhenServiceReturnsActivation()
        {
            // Arrange
            var muscleToCreate = TestData.Activations.ActivationWithoutId;
            var createdActivation = TestData.Activations.ActivationWithoutId;
            createdActivation.Id = "1234";
            ActivationsRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Activation>()))
                .ReturnsAsync(createdActivation);

            // Act
            var result = await ActivationsService.CreateOneAsync(muscleToCreate);

            // Assert
            Assert.Same(createdActivation, result);
        }

        [Fact]
        public async void CreateOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            var muscleToCreate = TestData.Activations.ActivationWithoutId;
            ActivationsRepositoryMock
                .Setup(x => x.CreateOneAsync(It.IsAny<Activation>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ActivationsService.CreateOneAsync(muscleToCreate);

            // Assert
            Assert.Null(result);
        }

        #endregion

        #region Task<Activation> DeleteOneAsync(string id)

        [Fact]
        public async void DeleteOneAsync_ReturnsActivation_WhenRepositoryReturnsActivation()
        {
            // Arrange
            var deletedActivation = TestData.Activations.ContractActivation;
            ActivationsRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync(deletedActivation);

            // Act
            var result = await ActivationsService.DeleteOneAsync("123021");

            // Assert
            Assert.Same(deletedActivation, result);
        }

        [Fact]
        public async void DeleteOneAsync_ReturnsNull_WhenRepositoryReturnsNull()
        {
            // Arrange
            ActivationsRepositoryMock
                .Setup(x => x.DeleteOneAsync(It.IsAny<string>()))
                .ReturnsAsync((Activation)null);

            // Act
            var result = await ActivationsService.DeleteOneAsync("123021");

            // Assert
            Assert.Null(result);
        }

        #endregion
    }
}

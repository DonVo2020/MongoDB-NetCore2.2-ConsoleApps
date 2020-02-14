using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers;

using Mongo2Go;

using MongoDB.Driver;

using Shouldly;

using System;
using System.Collections.Generic;
using System.Linq;

using Xunit;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.RepositoryTests
{
    [Trait("Repository", nameof(ActivationsRepositoryTests))]
    public class ActivationsRepositoryTests : IDisposable
    {
        private IMongoDatabase Database { get; }
        private IMongoCollection<Activation> Collection { get; }
        private MongoDbRunner Runner { get; }
        private MongoClient MongoClient { get; }
        private ActivationsRepository ActivationsRepository { get; }

        public ActivationsRepositoryTests()
        {
            Runner = MongoDbRunner.StartForDebugging();
            MongoClient = new MongoClient(Runner.ConnectionString);
            MongoClient.DropDatabase("ActivationsTestDatabase");
            Database = MongoClient.GetDatabase("ActivationsTestDatabase");
            Collection = Database.GetCollection<Activation>("ActivationsTestCollection");

            ActivationsRepository = new ActivationsRepository(Collection);
        }

        public void Dispose()
        {
            MongoClient.DropDatabase("ActivationsTestDatabase");
            Runner.Dispose();
        }

        #region Task<Activation> ReadOneAsync(string id)

        [Fact]
        public async void ReadOneAsync_ByValidActivationId_ReturnsExpectedActivation200()
        {
            // Arrange
            var expectedActivation = TestData.Activations.ContractActivation;
            Collection.InsertOne(expectedActivation);

            // Act
            var result = await ActivationsRepository.ReadOneAsync(expectedActivation.Id);

            // Assert
            result.ShouldBe(expectedActivation);
        }

        [Fact]
        public async void ReadOneAsync_ByNonExistentActivationId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.ReadOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void ReadOneAsync_ByNon24BitHexActivationId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.ReadOneAsync("ThisIsNotA24BitHexStringValue");

            // Assert
            result.ShouldBeNull();
        }

        #endregion

        #region Read Many Activations

        [Fact]
        public async void ReadManyAsync_WhenMatchingDataExists_ReturnsListOfExpectedActivations()
        {
            // Arrange
            var expectedActivation = TestData.Activations.ContractActivation;
            var allActivations = TestData.Activations.ContractActivations;
            Collection.InsertMany(allActivations);

            // Act
            var result = await ActivationsRepository.ReadManyAsync(expectedActivation.ExerciseId);

            // Assert
            result.ShouldContain(expectedActivation);
        }

        [Fact]
        public async void ReadManyAsync_WhenNoMatchingDataExists_ReturnsEmptyActivationList()
        {
            // Arrange
            var allActivations = TestData.Activations.ContractActivations;
            Collection.InsertMany(allActivations);

            // Act
            var result = await ActivationsRepository.ReadManyAsync("NonExistentExerciseId");

            // Assert
            result.ShouldBeEmpty();
        }

        #endregion

        #region Task<IEnumerable<Activation>> ReadAllAsync()

        [Fact]
        public async void ReadAllAsync_WhenDataExists_ReturnsListOfExpectedActivations()
        {
            // Arrange
            var expectedActivations = TestData.Activations.ContractActivations;
            Collection.InsertMany(expectedActivations);

            // Act
            var result = await ActivationsRepository.ReadAllAsync();

            // Assert
            result.ShouldBe(expectedActivations);
        }

        [Fact]
        public async void ReadAllAsync_WhenCollectionIsEmpty_ReturnsEmptyActivationList()
        {
            // Act
            var result = await ActivationsRepository.ReadAllAsync();

            // Assert
            result.ShouldBeOfType(typeof(List<Activation>));
            result.ShouldBeEmpty();
        }

        #endregion

        #region Task<Activation> CreateOneAsync(Activation Activation)

        [Fact]
        public async void CreateOneAsync_ReturnsCreatedActivationWithId_WhenObjectIsInserted()
        {
            // Arrange
            var activationToCreate = TestData.Activations.ActivationWithoutId;

            // Act
            var result = await ActivationsRepository.CreateOneAsync(activationToCreate);

            // Assert
            result.Id.ShouldNotBeNull();
            result.ShouldBe(activationToCreate);
        }

        #endregion

        #region Task<Activation> DeleteOneAsync(string id)

        [Fact]
        public async void DeleteOneAsync_ByValidActivationId_ReturnsDeletedActivation()
        {
            // Arrange
            var activations = TestData.Activations.ContractActivations;
            var activationToDelete = activations.First();
            Collection.InsertMany(activations);

            // Act
            var result = await ActivationsRepository.DeleteOneAsync(activationToDelete.Id);

            // Assert
            result.ShouldBe(activationToDelete);
            Collection.ShouldNotContain(activationToDelete);
            Collection.ShouldNotBeEmpty();
        }

        [Fact]
        public async void DeleteOneAsync_ByNonExistentId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.DeleteOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void DeleteOneAsync_ByNon24BitHexId_ReturnsNull()
        {
            // Act
            var result = await ActivationsRepository.DeleteOneAsync("ThisIsNonHex");

            // Asssert
            result.ShouldBeNull();
        }

        #endregion
    }
}

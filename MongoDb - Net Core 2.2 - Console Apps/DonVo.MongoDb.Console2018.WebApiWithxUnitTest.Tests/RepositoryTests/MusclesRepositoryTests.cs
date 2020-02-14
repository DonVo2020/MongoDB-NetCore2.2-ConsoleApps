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
    [Trait("Repository", nameof(MusclesRepositoryTests))]
    public class MusclesRepositoryTests : IDisposable
    {
        private IMongoDatabase Database { get; }
        private IMongoCollection<Muscle> Collection { get; }
        private MongoDbRunner Runner { get; }
        private MongoClient MongoClient { get; }
        private MusclesRepository MusclesRepository { get; }

        public MusclesRepositoryTests()
        {
            Runner = MongoDbRunner.StartForDebugging();
            MongoClient = new MongoClient(Runner.ConnectionString);
            MongoClient.DropDatabase("musclesTestDatabase");
            Database = MongoClient.GetDatabase("musclesTestDatabase");
            Collection = Database.GetCollection<Muscle>("musclesTestCollection");

            MusclesRepository = new MusclesRepository(Collection);
        }

        public void Dispose()
        {
            MongoClient.DropDatabase("musclesTestDatabase");
            Runner.Dispose();
        }

        #region Task<Muscle> ReadOneAsync(string id)

        [Fact]
        public async void ReadOneAsync_ByValidMuscleId_ReturnsExpectedMuscle200()
        {
            // Arrange
            var expectedMuscle = TestData.Muscles.ContractMuscle;
            Collection.InsertOne(expectedMuscle);

            // Act
            var result = await MusclesRepository.ReadOneAsync(expectedMuscle.Id);

            // Assert
            result.ShouldBe(expectedMuscle);
        }

        [Fact]
        public async void ReadOneAsync_ByNonExistentMuscleId_ReturnsNull()
        {
            // Act
            var result = await MusclesRepository.ReadOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void ReadOneAsync_ByNon24BitHexMuscleId_ReturnsNull()
        {
            // Act
            var result = await MusclesRepository.ReadOneAsync("ThisIsNotA24BitHexStringValue");

            // Assert
            result.ShouldBeNull();
        }

        #endregion

        #region Task<IEnumerable<Muscle>> ReadAllAsync()

        [Fact]
        public async void ReadAllAsync_WhenDataExists_ReturnsListOfExpectedMuscles()
        {
            // Arrange
            var expectedMuscles = TestData.Muscles.ContractMuscles;
            Collection.InsertMany(expectedMuscles);

            // Act
            var result = await MusclesRepository.ReadAllAsync();

            // Assert
            result.ShouldBe(expectedMuscles);
        }

        [Fact]
        public async void ReadAllAsync_WhenCollectionIsEmpty_ReturnsListOfExpectedMuscles()
        {
            // Act
            var result = await MusclesRepository.ReadAllAsync();

            // Assert
            result.ShouldBeOfType(typeof(List<Muscle>));
            result.ShouldBeEmpty();
        }

        #endregion

        #region Task<Muscle> CreateOneAsync(Muscle muscle)

        [Fact]
        public async void CreateOneAsync_ReturnsCreatedMuscleWithId_WhenObjectIsInserted()
        {
            // Arrange
            var muscleToCreate = TestData.Muscles.MuscleWithoutId;

            // Act
            var result = await MusclesRepository.CreateOneAsync(muscleToCreate);

            // Assert
            result.Id.ShouldNotBeNull();
            result.ShouldBe(muscleToCreate);
        }

        #endregion

        #region Task<Muscle> DeleteOneAsync(string id)

        [Fact]
        public async void DeleteOneAsync_ByValidMuscleId_ReturnsDeletedMuscle()
        {
            // Arrange
            var muscles = TestData.Muscles.ContractMuscles;
            var muscleToDelete = muscles.First();
            Collection.InsertMany(muscles);

            // Act
            var result = await MusclesRepository.DeleteOneAsync(muscleToDelete.Id);

            // Assert
            result.ShouldBe(muscleToDelete);
            Collection.ShouldNotContain(muscleToDelete);
            Collection.ShouldNotBeEmpty();
        }

        [Fact]
        public async void DeleteOneAsync_ByNonExistentId_ReturnsNull()
        {
            // Act
            var result = await MusclesRepository.DeleteOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void DeleteOneAsync_ByNon24BitHexId_ReturnsNull()
        {
            // Act
            var result = await MusclesRepository.DeleteOneAsync("ThisIsNonHex");

            // Asssert
            result.ShouldBeNull();
        }

        #endregion
    }
}

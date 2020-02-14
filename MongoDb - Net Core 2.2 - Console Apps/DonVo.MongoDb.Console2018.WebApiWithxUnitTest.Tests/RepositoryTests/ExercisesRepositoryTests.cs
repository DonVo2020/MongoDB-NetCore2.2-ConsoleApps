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
    [Trait("Repository", nameof(ExercisesRepositoryTests))]
    public class ExercisesRepositoryTests : IDisposable
    {
        private IMongoDatabase Database { get; }
        private IMongoCollection<Exercise> Collection { get; }
        private MongoDbRunner Runner { get; }
        private MongoClient MongoClient { get; }
        private ExercisesRepository ExercisesRepository { get; }

        public ExercisesRepositoryTests()
        {
            Runner = MongoDbRunner.StartForDebugging();
            MongoClient = new MongoClient(Runner.ConnectionString);
            MongoClient.DropDatabase("exercisesTestDatabase");
            Database = MongoClient.GetDatabase("exercisesTestDatabase");
            Collection = Database.GetCollection<Exercise>("exercisesTestCollection");

            ExercisesRepository = new ExercisesRepository(Collection);
        }

        public void Dispose()
        {
            MongoClient.DropDatabase("exercisesTestDatabase");
            Runner.Dispose();
        }

        #region Task<Exercise> ReadOneAsync(string id)

        [Fact]
        public async void ReadOneAsync_ByValidExerciseId_ReturnsExpectedExercise200()
        {
            // Arrange
            var expectedExercise = TestData.Exercises.ContractExercise;
            Collection.InsertOne(expectedExercise);

            // Act
            var result = await ExercisesRepository.ReadOneAsync(expectedExercise.Id);

            // Assert
            result.ShouldBe(expectedExercise);
        }

        [Fact]
        public async void ReadOneAsync_ByNonExistentExerciseId_ReturnsNull()
        {
            // Act
            var result = await ExercisesRepository.ReadOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void ReadOneAsync_ByNon24BitHexExerciseId_ReturnsNull()
        {
            // Act
            var result = await ExercisesRepository.ReadOneAsync("ThisIsNotA24BitHexStringValue");

            // Assert
            result.ShouldBeNull();
        }

        #endregion

        #region Task<IEnumerable<Exercise>> ReadAllAsync()

        [Fact]
        public async void ReadAllAsync_WhenDataExists_ReturnsListOfExpectedExercises()
        {
            // Arrange
            var expectedExercises = TestData.Exercises.ContractExercises;
            Collection.InsertMany(expectedExercises);

            // Act
            var result = await ExercisesRepository.ReadAllAsync();

            // Assert
            result.ShouldBe(expectedExercises);
        }

        [Fact]
        public async void ReadAllAsync_WhenCollectionIsEmpty_ReturnsListOfExpectedExercises()
        {
            // Act
            var result = await ExercisesRepository.ReadAllAsync();

            // Assert
            result.ShouldBeOfType(typeof(List<Exercise>));
            result.ShouldBeEmpty();
        }

        #endregion

        #region Task<Exercise> CreateOneAsync(Exercise exercise)

        [Fact]
        public async void CreateOneAsync_ReturnsCreatedExerciseWithId_WhenObjectIsInserted()
        {
            // Arrange
            var exerciseToCreate = TestData.Exercises.ExerciseWithoutId;

            // Act
            var result = await ExercisesRepository.CreateOneAsync(exerciseToCreate);

            // Assert
            result.Id.ShouldNotBeNull();
            result.ShouldBe(exerciseToCreate);
        }

        #endregion

        #region Task<Exercise> DeleteOneAsync(string id)

        [Fact]
        public async void DeleteOneAsync_ByValidExerciseId_ReturnsDeletedExercise()
        {
            // Arrange
            var exercises = TestData.Exercises.ContractExercises;
            var exerciseToDelete = exercises.First();
            Collection.InsertMany(exercises);

            // Act
            var result = await ExercisesRepository.DeleteOneAsync(exerciseToDelete.Id);

            // Assert
            result.ShouldBe(exerciseToDelete);
            Collection.ShouldNotContain(exerciseToDelete);
            Collection.ShouldNotBeEmpty();
        }

        [Fact]
        public async void DeleteOneAsync_ByNonExistentId_ReturnsNull()
        {
            // Act
            var result = await ExercisesRepository.DeleteOneAsync(Utilities.GetRandomHexString());

            // Assert
            result.ShouldBeNull();
        }

        [Fact]
        public async void DeleteOneAsync_ByNon24BitHexId_ReturnsNull()
        {
            // Act
            var result = await ExercisesRepository.DeleteOneAsync("ThisIsNonHex");

            // Asssert
            result.ShouldBeNull();
        }

        #endregion
    }
}

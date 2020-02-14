using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Mongo2Go;
using MongoDB.Driver;
using RestEase;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Bingo.Specification.IntegrationTests.Support
{
    public class ServiceFixture : IDisposable
    {
        public ServiceFixture()
        {
            InitializeMongo();
            LoadTestData();

            InitializeWebHost();

            InitializeApiInterface();
        }

        public void Dispose()
        {
            MongoClient.DropDatabase("BingoTestDatabase");
            Runner.Dispose();
        }

        public void InitializeMongo()
        {
            Runner = MongoDbRunner.StartForDebugging();
            MongoClient = new MongoClient(Runner.ConnectionString);

            MongoClient.DropDatabase("BingoTestDatabase");

            Database = MongoClient.GetDatabase("BingoTestDatabase");
            ExercisesCollection = Database.GetCollection<Exercise>("exercises");
            MusclesCollection = Database.GetCollection<Muscle>("muscles");
            ActivationsCollection = Database.GetCollection<Activation>("activations");
        }

        public void InitializeWebHost()
        {
            HttpServer = new TestServer(new WebHostBuilder()
               .UseStartup<TestStartup>()
               .ConfigureServices(ConfigureServices));

            HttpClient = HttpServer.CreateClient();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(ExercisesCollection);
            services.AddSingleton(MusclesCollection);
            services.AddSingleton(ActivationsCollection);
        }

        public void InitializeApiInterface()
        {
            Api = RestClient.For<IBingoApi>(HttpClient);
        }

        public void LoadTestData()
        {
            IEnumerable<Exercise> exercises = DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.TestData.Exercises.GetAllExercisesForCollection();
            ExercisesCollection.InsertMany(exercises);

            IEnumerable<Muscle> muscles = DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.TestData.Muscles.GetAllMusclesForCollection();
            MusclesCollection.InsertMany(muscles);

            IEnumerable<Activation> activations = DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.TestData.Activations.GetAllActivationsForCollection();
            ActivationsCollection.InsertMany(activations);
        }

        public IBingoApi Api { get; set; }
        public IMongoCollection<Exercise> ExercisesCollection { get; set; }
        public IMongoCollection<Muscle> MusclesCollection { get; set; }
        public IMongoCollection<Activation> ActivationsCollection { get; set; }

        private MongoDbRunner Runner { get; set; }
        private MongoClient MongoClient { get; set; }
        private IMongoDatabase Database { get; set; }

        private TestServer HttpServer { get; set; }
        private HttpClient HttpClient { get; set; }

    }
}

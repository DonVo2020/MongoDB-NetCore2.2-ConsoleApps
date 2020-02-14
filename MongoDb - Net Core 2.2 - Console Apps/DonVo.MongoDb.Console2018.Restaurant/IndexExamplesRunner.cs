using DonVo.MongoDb.Console2018.Restaurant.Data;
using DonVo.MongoDb.Console2018.Restaurant.Data.Models;
using DonVo.MongoDb.Console2018.Restaurant.Infrastructure;
using MongoDB.Driver;
using System;

namespace DonVo.MongoDb.Console2018.Restaurant
{
    public class IndexExamplesRunner
    {
        public void RunViewIndexingExamples()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            var zipCodesIndexManager = modelContext.ZipCodes.Indexes;
            var restaurantsIndexManager = modelContext.Restaurants.Indexes;

            var zipCodesIndexList = zipCodesIndexManager.List();
            var restaurantsIndexList = restaurantsIndexManager.List();
            while (restaurantsIndexList.MoveNext())
            {
                var currentIndex = restaurantsIndexList.Current;
                foreach (var doc in currentIndex)
                {
                    var docNames = doc.Names;
                    foreach (string name in docNames)
                    {
                        var value = doc.GetValue(name);
                        Console.WriteLine(string.Concat(name, ": ", value));
                    }
                }
            }
        }

        public void RunIndexCreationExamples()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            var restaurantsIndexManager = modelContext.Restaurants.Indexes;
            var restaurantIndexDefinition = Builders<RestaurantDb>.IndexKeys.Ascending(r => r.Id);
            string result = restaurantsIndexManager.CreateOne(restaurantIndexDefinition, new CreateIndexOptions() { Name = "RestaurantIdIndexAsc", Background = true });
            Console.WriteLine("\nCreated an Index Name: " + result);
        }

        public void RunDropIndexExamples()
        {
            Console.WriteLine("\nIndex: RestaurantIdIndexAsc is dropped.");
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            var restaurantsIndexManager = modelContext.Restaurants.Indexes;
            Console.WriteLine(restaurantsIndexManager.Settings.WriteConcern.ToBsonDocument());
            restaurantsIndexManager.DropOne("RestaurantIdIndexAsc");
        }
    }
}

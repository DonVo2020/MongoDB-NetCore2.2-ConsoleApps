using DonVo.MongoDb.Console2018.Restaurant.Data;
using DonVo.MongoDb.Console2018.Restaurant.Data.Models;
using DonVo.MongoDb.Console2018.Restaurant.Infrastructure;

using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Restaurant
{

    public class CrudExampleRunner
    {
        public void Updates()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            var updateDefinitionBorough = Builders<RestaurantDb>.Update.Set(r => r.Borough, "New Borough");
            var updateDefinitionGrades = Builders<RestaurantDb>.Update.Push(r => r.Grades, new RestaurantGradeDb() { Grade = "A", InsertedUtc = DateTime.UtcNow, Score = "6" });
            var combinedUpdateDefinition = Builders<RestaurantDb>.Update.Combine(updateDefinitionBorough, updateDefinitionGrades);
            UpdateResult updateResult = modelContext.Restaurants.UpdateOne(r => r.Name == "BrandNewMexicanKing", combinedUpdateDefinition, new UpdateOptions() { IsUpsert = true });
            Console.WriteLine("Updates() with IsAcknowledged: {0}; MatchedCount: {1}; and ModifiedCount: {2}", updateResult.IsAcknowledged, updateResult.MatchedCount, updateResult.ModifiedCount);
        }

        public void BulkWrites()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            RestaurantDb newThaiRestaurant = new RestaurantDb();
            newThaiRestaurant.Address = new RestaurantAddressDb()
            {
                BuildingNr = "150",
                Coordinates = new double[] { 22.82, 99.12 },
                Street = "Old Street",
                ZipCode = 876654
            };
            newThaiRestaurant.Borough = "Somewhere in Thailand";
            newThaiRestaurant.Cuisine = "Thai";
            newThaiRestaurant.Grades = new List<RestaurantGradeDb>()
            {
                new RestaurantGradeDb() {Grade = "A", InsertedUtc = DateTime.UtcNow, Score = "7" },
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "4" },
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "10" },
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "4" }
            };
            newThaiRestaurant.Id = 463456435;
            newThaiRestaurant.Name = "FavThai";

            RestaurantDb newThaiRestaurantTwo = new RestaurantDb();
            newThaiRestaurantTwo.Address = new RestaurantAddressDb()
            {
                BuildingNr = "13",
                Coordinates = new double[] { 22.82, 99.12 },
                Street = "Wide Street",
                ZipCode = 345456
            };
            newThaiRestaurantTwo.Borough = "Somewhere in Thailand";
            newThaiRestaurantTwo.Cuisine = "Thai";
            newThaiRestaurantTwo.Grades = new List<RestaurantGradeDb>()
            {
                new RestaurantGradeDb() {Grade = "A", InsertedUtc = DateTime.UtcNow, Score = "4" },
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "10" },
                new RestaurantGradeDb() {Grade = "C", InsertedUtc = DateTime.UtcNow, Score = "4" }
            };
            newThaiRestaurantTwo.Id = 463456436;
            newThaiRestaurantTwo.Name = "ThaiDinner";

            var updateDefinitionBorough = Builders<RestaurantDb>.Update.Set(r => r.Borough, "The middle of Thailand");

            FilterDefinition<RestaurantDb> deletionFilterDefinition = Builders<RestaurantDb>.Filter.Eq(r => r.Name, "PakistaniKing");

            BulkWriteResult bulkWriteResult = modelContext.Restaurants.BulkWrite(new WriteModel<RestaurantDb>[]
            {
                new InsertOneModel<RestaurantDb>(newThaiRestaurant),
                new InsertOneModel<RestaurantDb>(newThaiRestaurantTwo),
                new UpdateOneModel<RestaurantDb>(Builders<RestaurantDb>.Filter.Eq(r => r.Name, "RandomThai"), updateDefinitionBorough),
                new DeleteOneModel<RestaurantDb>(deletionFilterDefinition)
            }, new BulkWriteOptions() { IsOrdered = false });

            Console.WriteLine(string.Concat("Deleted count: ", bulkWriteResult.DeletedCount));
            Console.WriteLine(string.Concat("Inserted count: ", bulkWriteResult.InsertedCount));
            Console.WriteLine(string.Concat("Acknowledged: ", bulkWriteResult.IsAcknowledged));
            Console.WriteLine(string.Concat("Matched count: ", bulkWriteResult.MatchedCount));
            Console.WriteLine(string.Concat("Modified count: ", bulkWriteResult.ModifiedCount));
            Console.WriteLine(string.Concat("Request count: ", bulkWriteResult.RequestCount));
            Console.WriteLine(string.Concat("Upsert count: ", bulkWriteResult.Upserts.Count));
        }

        public void Deletions()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            //DeleteResult deleteResult = modelContext.Restaurants.DeleteOne(Builders<RestaurantDb>.Filter.Eq(r => r.Name, "BrandNewMexicanKing"));
            FindOneAndDeleteOptions<RestaurantDb, RestaurantDb> findOneAndDeleteOptions = new FindOneAndDeleteOptions<RestaurantDb, RestaurantDb>()
            {
                Sort = Builders<RestaurantDb>.Sort.Descending(r => r.Id)
            };
            RestaurantDb deleted = modelContext.Restaurants.FindOneAndDelete(Builders<RestaurantDb>.Filter.Eq(r => r.Name, "BrandNewMexicanKing"), findOneAndDeleteOptions);

            if(deleted != null)
                Console.WriteLine("Deleted.Name: " + deleted.Name);
            else
                Console.WriteLine("No record found to delete.");
        }

        public void Replacements()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            RestaurantDb mexicanReplacement = new RestaurantDb();
            mexicanReplacement.Address = new RestaurantAddressDb()
            {
                BuildingNr = "4/D",
                Coordinates = new double[] { 24.68, -100.9 },
                Street = "New Mexico Street",
                ZipCode = 768324865
            };
            mexicanReplacement.Borough = "In the middle of Mexico";
            mexicanReplacement.Cuisine = "Mexican";
            mexicanReplacement.Grades = new List<RestaurantGradeDb>()
            {
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "10" },
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "4" }
            };
            mexicanReplacement.Id = 463456436;
            mexicanReplacement.Name = "BrandNewMexicanKing";
            mexicanReplacement.MongoDbId = ObjectId.Parse("5c18892efbae12fea9dad83b");

            //ReplaceOneResult replaceOneResult = modelContext.Restaurants.ReplaceOne(Builders<RestaurantDb>.Filter.Eq(r => r.Name, "MexicanKing"), mexicanReplacement, new UpdateOptions() { IsUpsert = true });
            //Console.WriteLine(replaceOneResult.IsAcknowledged);
            //Console.WriteLine(replaceOneResult.MatchedCount);
            //Console.WriteLine(replaceOneResult.ModifiedCount);
            //Console.WriteLine(replaceOneResult.UpsertedId);

            RestaurantDb replaced = modelContext.Restaurants.FindOneAndReplace
                (Builders<RestaurantDb>.Filter.Eq(r => r.Name, "NewMexicanKing"),
                mexicanReplacement,
                new FindOneAndReplaceOptions<RestaurantDb, RestaurantDb>()
                {
                    IsUpsert = true,
                    ReturnDocument = ReturnDocument.After,
                    Sort = Builders<RestaurantDb>.Sort.Descending(r => r.Name)
                });
        }
        public void SearchesAndInsertions()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            var boroughFilter = Builders<RestaurantDb>.Filter.Eq(r => r.Borough, "Brooklyn");
            var idSortDefinition = Builders<RestaurantDb>.Sort.Ascending(r => r.Id);
            var restaurantsInBrooklyn = modelContext.Restaurants.Find(boroughFilter).Limit(5).Sort(idSortDefinition).ToList();
            var cuisineFilter = Builders<RestaurantDb>.Filter.Eq(r => r.Cuisine, "Delicatessen");
            var cuisineAndBoroughFilter = boroughFilter & cuisineFilter;
            var cuisineAndBoroughFilterAlternative = Builders<RestaurantDb>.Filter.And(boroughFilter, cuisineFilter);
            var firstRes = modelContext.Restaurants.Find(cuisineAndBoroughFilterAlternative).FirstOrDefault();
            Console.WriteLine(firstRes);
            var restaurants = modelContext.Restaurants.FindSync<RestaurantDb>(boroughFilter).ToList();
            var restaurant = modelContext.Restaurants.FindSync<RestaurantDb>(boroughFilter).FirstOrDefault();

            var firstResWithLinq = modelContext.Restaurants.Find(r => r.Borough == "Brooklyn" && r.Cuisine == "Delicatessen").FirstOrDefault();
            Console.WriteLine(firstResWithLinq);

            var arrayFilterGradeA = Builders<RestaurantDb>.Filter.ElemMatch(r => r.Grades, g => g.Grade == "A");
            var arrayFilterGradeB = Builders<RestaurantDb>.Filter.ElemMatch(r => r.Grades, g => g.Grade == "B");
            var arrayFilterGradeC = Builders<RestaurantDb>.Filter.ElemMatch(r => r.Grades, g => g.Grade == "C");
            var arrayFilterWithAllGrades = arrayFilterGradeA & arrayFilterGradeB & arrayFilterGradeC;
            var firstResWithAllGrades = modelContext.Restaurants.Find(arrayFilterWithAllGrades).FirstOrDefault();
            Console.WriteLine(firstResWithAllGrades);

            Console.WriteLine(restaurant);
            FindOptions findOpts = new FindOptions();

            ZipCodeDb firstInMassachusetts = modelContext.ZipCodes.FindSync(z => z.State == "MA").FirstOrDefault();
            Console.WriteLine(firstInMassachusetts);

            ZipCodeDb firstZip = modelContext.ZipCodes.Find(z => true).FirstOrDefault();
            RestaurantDb firstRestaurant = modelContext.Restaurants.Find(r => true).FirstOrDefault();

            List<ZipCodeDb> allZipCodes = modelContext.ZipCodes.Find(z => true).ToList();
            List<RestaurantDb> allRestaurants = modelContext.Restaurants.Find(r => true).ToList();

            Console.WriteLine(firstZip);
            Console.WriteLine(firstRestaurant);

            var zipSortByState = Builders<ZipCodeDb>.Sort.Ascending(z => z.State);
            var zipSortByName = Builders<ZipCodeDb>.Sort.Descending(z => z.City);
            var combinedSort = Builders<ZipCodeDb>.Sort.Combine(zipSortByState, zipSortByName);
            FindOptions fo = new FindOptions();

            var first100zips = modelContext.ZipCodes.Find(z => true).Limit(100).Sort(combinedSort).ToList();

            var zipCombinedSortAlternative = Builders<ZipCodeDb>.Sort.Ascending(z => z.State).Descending(z => z.City);
            var first100zipsAlternative = modelContext.ZipCodes.Find(z => true).Limit(100).Sort(zipCombinedSortAlternative).ToList();

            var first100zipsLinqSolution = modelContext.ZipCodes.Find(z => true).Limit(100).SortBy(z => z.State).ThenByDescending(z => z.City).ToList();

            RestaurantDb newRestaurant = new RestaurantDb();
            newRestaurant.Address = new RestaurantAddressDb()
            {
                BuildingNr = "120",
                Coordinates = new double[] { 22.82, 99.12 },
                Street = "Whatever",
                ZipCode = 123456
            };
            newRestaurant.Borough = "Somewhere in Thailand";
            newRestaurant.Cuisine = "Thai";
            newRestaurant.Grades = new List<RestaurantGradeDb>()
            {
                new RestaurantGradeDb() {Grade = "A", InsertedUtc = DateTime.UtcNow, Score = "7" },
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "4" }
            };
            newRestaurant.Id = 883738291;
            newRestaurant.Name = "RandomThai";

            modelContext.Restaurants.InsertOne(newRestaurant);

            RestaurantDb newPakistaniRestaurant = new RestaurantDb();
            newPakistaniRestaurant.Address = new RestaurantAddressDb()
            {
                BuildingNr = "12A",
                Coordinates = new double[] { 31.135, 71.24 },
                Street = "New Street",
                ZipCode = 9877654
            };
            newPakistaniRestaurant.Borough = "Somewhere in Pakistan";
            newPakistaniRestaurant.Cuisine = "Pakistani";
            newPakistaniRestaurant.Grades = new List<RestaurantGradeDb>()
            {
                new RestaurantGradeDb() {Grade = "A", InsertedUtc = DateTime.UtcNow, Score = "9" },
                new RestaurantGradeDb() {Grade = "C", InsertedUtc = DateTime.UtcNow, Score = "3" }
            };
            newPakistaniRestaurant.Id = 457656745;
            newPakistaniRestaurant.Name = "PakistaniKing";

            RestaurantDb newMexicanRestaurant = new RestaurantDb();
            newMexicanRestaurant.Address = new RestaurantAddressDb()
            {
                BuildingNr = "2/C",
                Coordinates = new double[] { 24.68, -100.9 },
                Street = "Mexico Street",
                ZipCode = 768324523
            };
            newMexicanRestaurant.Borough = "Somewhere in Mexico";
            newMexicanRestaurant.Cuisine = "Mexican";
            newMexicanRestaurant.Grades = new List<RestaurantGradeDb>()
            {
                new RestaurantGradeDb() {Grade = "B", InsertedUtc = DateTime.UtcNow, Score = "10" }
            };
            newMexicanRestaurant.Id = 457656745;
            newMexicanRestaurant.Name = "MexicanKing";

            List<RestaurantDb> newRestaurants = new List<RestaurantDb>()
            {
                newPakistaniRestaurant,
                newMexicanRestaurant
            };
            InsertOneOptions opts = new InsertOneOptions();

            modelContext.Restaurants.InsertMany(newRestaurants);

            Console.ReadKey();
        }
    }
}

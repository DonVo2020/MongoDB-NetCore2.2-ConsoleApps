using DonVo.MongoDb.Console2018.Restaurant.Data.Models;
using DonVo.MongoDb.Console2018.Restaurant.Infrastructure;

using MongoDB.Driver;

using System;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Restaurant.Data
{
    public class ModelContext
    {
        private IMongoClient Client { get; set; }
        private IMongoDatabase Database { get; set; }
        private IConfigurationRepository ConfigurationRepository { get; set; }
        private static ModelContext _modelContext;

        private ModelContext() { }

        public static ModelContext Create(IConfigurationRepository configurationRepository,
            IConnectionStringRepository connectionStringRepository)
        {
            if (configurationRepository == null) throw new ArgumentNullException("ConfigurationRepository");
            if (connectionStringRepository == null) throw new ArgumentNullException("ConnectionStringRepository");
            if (_modelContext == null)
            {
                _modelContext = new ModelContext();
                string connectionString = connectionStringRepository.ReadConnectionString("MongoDb");

                //set read and write concern on client level
                MongoClientSettings settings = new MongoClientSettings();
                settings.ReadConcern = new ReadConcern(ReadConcernLevel.Majority);
                Tag tag = new Tag("tagName", "tagValue");
                TagSet tagSet = new TagSet(new List<Tag>() { tag });
                settings.ReadPreference = new ReadPreference(ReadPreferenceMode.Secondary, new List<TagSet>() { tagSet });
                WriteConcern writeConcernWithNumberOfServers = new WriteConcern(2, TimeSpan.FromSeconds(60), false, false);
                WriteConcern writeConcerntWithMajority = new WriteConcern("majority", TimeSpan.FromSeconds(60), false, false);
                settings.WriteConcern = writeConcerntWithMajority;

                //set read and write concern on DB level
                MongoDatabaseSettings dbSettings = new MongoDatabaseSettings();


                _modelContext.Client = new MongoClient(connectionString);
                _modelContext.Database = _modelContext.Client.GetDatabase(configurationRepository.GetConfigurationValue("DemoDatabaseName", "model"));

                _modelContext.ConfigurationRepository = configurationRepository;
            }
            return _modelContext;
        }

        public void TestConnection()
        {
            var dbsCursor = _modelContext.Client.ListDatabases();
            var dbsList = dbsCursor.ToList();
            foreach (var db in dbsList)
            {
                Console.WriteLine(db);
            }
        }

        public IMongoCollection<RestaurantDb> Restaurants
        {

            get
            {
                //set read and write concern on collection level
                MongoCollectionSettings collectionSettings = new MongoCollectionSettings();

                return Database.GetCollection<RestaurantDb>(ConfigurationRepository.GetConfigurationValue("RestaurantsCollectionName", "restaurants"));
            }
        }

        public IMongoCollection<ZipCodeDb> ZipCodes
        {
            get { return Database.GetCollection<ZipCodeDb>(ConfigurationRepository.GetConfigurationValue("ZipCodesCollectionName", "¨zipcodes")); }
        }
    }
}

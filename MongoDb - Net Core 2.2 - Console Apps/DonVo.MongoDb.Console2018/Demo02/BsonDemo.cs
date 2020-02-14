using MongoDB.Bson;
using MongoDB.Driver;

using System;
using System.Linq.Expressions;
using System.Threading;

namespace DonVo.MongoDb.Console2018.Demo02
{
    public class BsonDemo
    {
        private static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        IMongoDatabase database = client.GetDatabase("BsonDemo");
        //public static void InsertOne(IMongoDatabase database)
        public void InsertOne()
        {
            var collection = database.GetCollection<BsonDocument>("doc1");
            var document1 = new BsonDocument
                                {
                                    { "name", "Deborah Mongo" },
                                    { "country", "New Zealand" },
                                    { "age", 33 },
                                    { "info", new BsonDocument { { "x", 203 }, { "y", 332 } } }
                                };
            var task = collection.InsertOneAsync(document1);
            task.Wait();

            Thread.Sleep(100);
            Console.WriteLine("doc1 has been inserted into BsonDemo database.\n");
        }

        //public void Query(IMongoDatabase database)
        public void Query()
        {
            Expression<Func<BsonDocument, bool>> query =
                doc => doc["age"] == 33;

            var collection = database.GetCollection<BsonDocument>("doc1");
            var result = collection
                .Find(query)
                .Limit(10)
                .ToListAsync()
                .Result;

            result.ForEach(Console.WriteLine);

            Console.Write("\n");
        }

        //public bool DeleteDatabase(string dbName)
        public bool DeleteDatabase()
        {
            try
            {
                client.DropDatabase("BsonDemo");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

using System;
using System.Linq;

namespace DonVo.MongoDb.Console2018.Demo03
{
    public class MappingDemo
    {
        private static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        static IMongoDatabase database = client.GetDatabase("BsonDemo");

        static MappingDemo()
        {
            BsonClassMap.RegisterClassMap<Invoice>(map =>
            {
                map.AutoMap();
                map.SetIdMember(map.GetMemberMap(invoice => invoice.Id));
                map.GetMemberMap(invoice => invoice.Lines).SetElementName("Items");
            });
        }

        public static bool DeleteDatabase()
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

        private static Invoice GetInvoice()
        {
            return new Invoice()
            {
                Id = "INV-001",
                Buyer = "Joe",
                Date = DateTime.Today,
                PayableInDays = 30,
                Lines = new[]
                            {
                                new InvoiceLine() { Name = "Learning MongoDB", Price = 12.99M },
                                new InvoiceLine() { Name = "MongoDB in Action", Price = 22.99M }
                            }
            };
        }

        //public static void InsertOne(IMongoDatabase database)
        public static void InsertOne()
        {
            var collection = database.GetCollection<Invoice>("doc3");
            var invoice = GetInvoice();

            collection.InsertOneAsync(invoice).Wait();
        }

        //public static void Query(IMongoDatabase database)
        public static void Query()
        {
            var collection = database.GetCollection<Invoice>("doc3");

            var invoices = collection
                .Find(inv => inv.Buyer == "Joe")
                .Limit(10)
                .ToListAsync()
                .Result;

            invoices.ForEach(invoice => Console.WriteLine(invoice.ToJson()));
        }

        //public static void UpdateOne(IMongoDatabase database)
        public static void UpdateOne()
        {
            var collection = database.GetCollection<Invoice>("doc3");
            var invoice = GetInvoice();

            var options = new FindOneAndReplaceOptions<Invoice>()
            {
                IsUpsert = true
            };
            collection
                .FindOneAndReplaceAsync<Invoice>(item => item.Id == invoice.Id, invoice, options)
                .Wait();
        }

        //public static void QueryAndAggregate(IMongoDatabase database)
        public static void QueryAndAggregate()
        {
            var collection = database.GetCollection<Invoice>("doc3");

            var items = collection
                .Aggregate()
                .Match(inv => inv.Buyer == "Joe")
                //.Group(inv => new { inv.Buyer }, g => g.Key)
                .Group(key => key.Buyer, value => new { Buyer = value.Key, Lines = value.Sum(key => key.Lines.Count) })
                .ToListAsync()
                .Result;

            items.ForEach(item => Console.WriteLine(item.ToJson()));

            Console.WriteLine("\n");          
        }
    }
}

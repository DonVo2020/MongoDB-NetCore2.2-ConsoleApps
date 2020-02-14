using DonVo.MongoDb.Console2018.Restaurant.Data;
using DonVo.MongoDb.Console2018.Restaurant.Data.Models;
using DonVo.MongoDb.Console2018.Restaurant.Infrastructure;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.MongoDb.Console2018.Restaurant
{
    public class AggregationExamplesRunner
    {
        public void RunAggregationWithAppend()
        {
            BsonDocument testBson = new BsonDocument { { "$group", new BsonDocument("_id", "$state")
                    .Add("population", new BsonDocument("$sum", "$pop")) } };
            Console.WriteLine(testBson);

            BsonDocument testBson2 = new BsonDocument { { "match", new BsonDocument("population", new BsonDocument("$gte", 5000000)) } };
            Console.WriteLine(testBson2);

            Console.WriteLine(new BsonDocument("$limit", 5));

            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());
            var looselyTypedRes = modelContext.ZipCodes.Aggregate()
                .AppendStage<BsonDocument>(new BsonDocument { { "$sort", new BsonDocument("_id", 1) } }).ToList();
            foreach (var item in looselyTypedRes)
            {
                var nameList = item.Names.ToList();
                foreach (string name in nameList)
                {
                    BsonValue bsonValue = item.GetValue(name);
                    Console.WriteLine(string.Format("{0}: {1}", name, bsonValue));
                }
            }

            var stronglyTypedRes = modelContext.ZipCodes.Aggregate()
                .AppendStage<ZipCodeDb>(new BsonDocument { { "$sort", new BsonDocument("_id", 1) } }).ToList();
            foreach (var item in stronglyTypedRes)
            {
                Console.WriteLine(item);
            }

            List<BsonDocument> looselyTypedAggregation = modelContext.ZipCodes.Aggregate()
                .AppendStage<BsonDocument>
                (
                    new BsonDocument { { "$group", new BsonDocument("_id", "$state")
                        .Add("population", new BsonDocument("$sum", "$pop")) } }
                )
                .AppendStage<BsonDocument>
                (
                    new BsonDocument { { "$match", new BsonDocument("population", new BsonDocument("$gte", 5000000)) } }
                )
                .AppendStage<BsonDocument>
                (
                    new BsonDocument { { "$sort", new BsonDocument("_id", 1) } }
                )
                .AppendStage<BsonDocument>
                (
                    new BsonDocument("$limit", 5)
                )
                .ToList();
            foreach (var item in looselyTypedAggregation)
            {
                var nameList = item.Names.ToList();
                foreach (string name in nameList)
                {
                    BsonValue bsonValue = item.GetValue(name);
                    Console.WriteLine(string.Format("{0}: {1}", name, bsonValue));
                }
            }
        }

        public void RunAggregationWithDedicatedMethods()
        {
            ModelContext modelContext = ModelContext.Create(new ConfigFileConfigurationRepository(), new AppConfigConnectionStringRepository());

            //anonymous object
            var result = modelContext.ZipCodes.Aggregate().Group(key => key.State,
                value => new { State = value.Key, Population = value.Sum(key => key.Population) }).ToList();

            foreach (var item in result)
            {
                Console.WriteLine(string.Format("{0}: {1}", item.State, item.Population));
            }

            //concrete object
            var resultWithAsFunction = modelContext.ZipCodes.Aggregate().Group(key => key.State,
                value => new { State = value.Key, Population = value.Sum(key => key.Population) })
                .As<ZipCodeGroupResult>().ToList();
            foreach (var item in resultWithAsFunction)
            {
                Console.WriteLine(string.Format("{0}: {1}", item.State, item.Population));
            }

            //concrete object with Select
            var resultWithSelectExtension = modelContext.ZipCodes.Aggregate().Group(key => key.State,
                value => new { State = value.Key, Population = value.Sum(key => key.Population) }).ToList()
                .Select(z => new ZipCodeGroupResult() { Population = z.Population, State = z.State });
            foreach (var item in resultWithSelectExtension)
            {
                Console.WriteLine(string.Format("{0}: {1}", item.State, item.Population));
            }

            //full query with anonymous object
            var fullQueryResultAnon = modelContext.ZipCodes.Aggregate().Group(key => key.State,
                value => new { State = value.Key, Population = value.Sum(key => key.Population) })
                .Match(z => z.Population > 5000000)
                .SortBy(z => z.State)
                .Limit(5)
                .ToList();
            foreach (var item in fullQueryResultAnon)
            {
                Console.WriteLine(string.Format("{0}: {1}", item.State, item.Population));
            }

            //full query with concrete object
            var fullQueryResultConcrete = modelContext.ZipCodes.Aggregate().Group(key => key.State,
                value => new { State = value.Key, Population = value.Sum(key => key.Population) })
                .Match(z => z.Population > 5000000)
                .SortBy(z => z.State)
                .Limit(5)
                .As<ZipCodeGroupResult>()
                .ToList();
            foreach (var item in fullQueryResultConcrete)
            {
                Console.WriteLine(string.Format("{0}: {1}", item.State, item.Population));
            }
        }
    }
}

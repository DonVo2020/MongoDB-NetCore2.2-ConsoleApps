using MongoDB.Driver;

using Shouldly;

using System;
using System.Collections.Generic;
using System.Linq;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Tests.Helpers
{
    public static class ShouldlyExtensions
    {
        private static List<T> SearchAll<T>(IMongoCollection<T> collection)
        {
            var filter = FilterDefinition<T>.Empty;
            return collection.FindAsync<T>(filter).Result.ToList();
        }

        public static void ShouldContain<T>(this IMongoCollection<T> collection, T expected)
        {
            var actual = SearchAll(collection);

            actual.ShouldContain(expected);
        }

        public static void ShouldNotContain<T>(this IMongoCollection<T> collection, T expected)
        {
            var actual = SearchAll(collection);

            actual.ShouldNotContain(expected);
        }

        public static void ShouldBeEmpty<T>(this IMongoCollection<T> collection)
        {
            var actual = SearchAll(collection);

            actual.Count.ShouldBe(0);
        }

        public static void ShouldNotBeEmpty<T>(this IMongoCollection<T> collection)
        {
            var actual = SearchAll(collection);

            actual.Count.ShouldBeGreaterThan(0);
        }

        public static void ShouldNotHaveNullDataMembers<T>(this object obj)
        {
            var result = typeof(T).GetProperties()
                .Select(prop => prop.GetValue(obj, null))
                .Where(val => val == null)
                .ToList();

            result.Count.ShouldBe(0, $"{typeof(object)} contains empty values: \n{obj.ToString()}");
        }

        public static void ShouldNotHaveNullDataMembersExcept<T>(this object obj, params string[] propertyNames)
        {
            var properties = typeof(T).GetProperties();

            foreach (System.Reflection.PropertyInfo prop in properties)
            {
                if (!propertyNames.Contains(prop.Name))
                    prop.GetValue(obj, null).ShouldNotBeNull($"{typeof(object)} contains empty values: \n{obj.ToString()}");
            }
        }
    }
}

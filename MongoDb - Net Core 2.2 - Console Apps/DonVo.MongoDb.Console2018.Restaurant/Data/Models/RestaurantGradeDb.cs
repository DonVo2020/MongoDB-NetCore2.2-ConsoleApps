using MongoDB.Bson.Serialization.Attributes;

using System;

namespace DonVo.MongoDb.Console2018.Restaurant.Data.Models
{
    [BsonIgnoreExtraElements]
    public class RestaurantGradeDb
    {
        [BsonElement(elementName: "date")]
        public DateTime InsertedUtc { get; set; }
        [BsonElement(elementName: "grade")]
        public string Grade { get; set; }
        [BsonElement(elementName: "score")]
        public object Score { get; set; }
    }
}

using MongoDB.Bson.Serialization.Attributes;

namespace DonVo.MongoDb.Console2018.Restaurant.Data.Models
{
    public class ZipCodeGroupResult
    {
        [BsonId]
        [BsonElement(elementName: "_id")]
        public string State { get; set; }
        [BsonElement(elementName: "Population")]
        public int Population { get; set; }
    }
}

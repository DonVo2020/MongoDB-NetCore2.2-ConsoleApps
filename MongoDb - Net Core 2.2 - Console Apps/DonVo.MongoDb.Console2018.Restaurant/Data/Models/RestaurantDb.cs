using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DonVo.MongoDb.Console2018.Restaurant.Data.Models
{
    [BsonIgnoreExtraElements]
    public class RestaurantDb
    {
        [BsonId]
        [BsonElement(elementName: "_id")]
        public ObjectId MongoDbId { get; set; }
        [BsonElement(elementName: "address")]
        public RestaurantAddressDb Address { get; set; }
        [BsonElement(elementName: "borough")]
        public string Borough { get; set; }
        [BsonElement(elementName: "cuisine")]
        public string Cuisine { get; set; }
        [BsonElement(elementName: "grades")]
        public IEnumerable<RestaurantGradeDb> Grades { get; set; }
        [BsonElement(elementName: "name")]
        public string Name { get; set; }
        [BsonElement(elementName: "restaurant_id")]
        [BsonRepresentation(BsonType.Double)]
        public int Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}

using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

namespace DonVo.MongoDb.Console2018.Restaurant.Data.Models
{
    [BsonIgnoreExtraElements]
    public class ZipCodeDb
    {
        [BsonId]
        [BsonElement(elementName: "_id")]
        public string Id { get; set; }
        [BsonElement(elementName: "city")]
        public string City { get; set; }
        [BsonElement(elementName: "loc")]
        public double[] Coordinates { get; set; }
        [BsonElement(elementName: "pop")]
        public int Population { get; set; }
        [BsonElement(elementName: "state")]
        public string State { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}

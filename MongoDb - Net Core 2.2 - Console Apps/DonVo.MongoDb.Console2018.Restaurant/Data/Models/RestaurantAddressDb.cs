using MongoDB.Bson.Serialization.Attributes;

namespace DonVo.MongoDb.Console2018.Restaurant.Data.Models
{
    [BsonIgnoreExtraElements]
    public class RestaurantAddressDb
    {
        [BsonElement(elementName: "building")]
        public string BuildingNr { get; set; }
        [BsonElement(elementName: "coord")]
        public double[] Coordinates { get; set; }
        [BsonElement(elementName: "street")]
        public string Street { get; set; }
        [BsonElement(elementName: "zipcode")]
        public object ZipCode { get; set; }
    }
}

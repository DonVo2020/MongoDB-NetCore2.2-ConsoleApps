using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

namespace DonVo.MongoDb.Console2018.Transactions.ViewModels.Mongo
{
    public class Entity
    {
        [BsonId]
        [JsonIgnore]
        public virtual ObjectId _id { get; set; }
    }
}

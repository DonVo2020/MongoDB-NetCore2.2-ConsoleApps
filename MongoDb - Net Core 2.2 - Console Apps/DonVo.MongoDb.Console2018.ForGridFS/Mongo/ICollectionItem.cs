using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DonVo.MongoDb.Console2018.ForGridFS.Mongo
{
    public interface ICollectionItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        string Id { get; set; }
    }
}

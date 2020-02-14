using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Helpers;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

using Newtonsoft.Json;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities
{
    public class Muscle : BingoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string LongName { get; set; }
        public string GroupId { get; set; }
        public string RegionId { get; set; }

        public override bool Equals(object obj)
        {
            return this.DeepEquals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}

using DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DonVo.MongoDb.Console2018.WebApiWithxUnitTest.Library.Repositories.Entities
{
    public class Exercise : BingoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }

        public ExerciseTypeEnum ExerciseType { get; set; }
    }
}

﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DonVo.MongoDb.Console2018.ForGridFS.Mongo
{
    public class ObjectRef<T> where T : DBObject<T>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonIgnore]
        public T Value
        {
            get => DBObject<T>.FirstOrDefault(x => x.Id == Id);
            set => Id = value.Id;
        }

        public static implicit operator ObjectRef<T>(T d) => new ObjectRef<T> { Id = d.Id };

        public override string ToString() => Value?.ToString() ?? Id;
    }
}

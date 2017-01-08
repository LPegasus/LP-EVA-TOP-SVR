using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace LP.Common.DB.Models
{
    public class DbProfile : IEntity
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string Remarks { get; set; }
        public string CreateBy { get; set; }
        [BsonDateTimeOptions(DateOnly = false, Kind = DateTimeKind.Local, Representation = BsonType.DateTime)]
        public DateTime CreateAt { get; set; }
        [BsonExtraElements]
        public BsonDocument MetaData { get; set; }
        public BsonDocument ExtData { get; set; }
    }
}

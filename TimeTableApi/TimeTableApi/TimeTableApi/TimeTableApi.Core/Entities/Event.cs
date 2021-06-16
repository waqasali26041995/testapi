using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableApi.Core.Entities
{
    public class Event : Generic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }


        [BsonElement("imageName")]
        public string ImageName { get; set; }

        [BsonElement("eventDate")]
        public DateTime EventDate { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("userId")]
        public string UserId { get; set; }
    }
}

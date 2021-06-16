using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableApi.Core.Entities
{
    public class Generic
    {
        [BsonElement("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }
        
        [BsonElement("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}

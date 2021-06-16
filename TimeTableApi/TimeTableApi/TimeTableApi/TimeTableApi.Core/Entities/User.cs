using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTableApi.Core.Entities
{
    public class User : Generic
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("roleId")]
        public string RoleId { get; set; }
    }
}

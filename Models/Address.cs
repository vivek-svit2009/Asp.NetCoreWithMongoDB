using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWithMongoDB.Models
{
    [BsonIgnoreExtraElements]
    public class Address
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        [BsonElement("City")]
        public string City { get; set; }
    }
}

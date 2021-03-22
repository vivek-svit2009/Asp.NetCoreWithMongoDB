
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPWithMongoDB.Models
{
    public class MyCustomArraySerializer : SerializerBase<Address>
    {
        public override Address Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartArray();
            var Id = context.Reader.ReadObjectId();
            var City = context.Reader.ReadString();
            context.Reader.ReadEndArray();

            return new Address() { Id = Id, City = City };
        }
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Address value)
        {
            context.Writer.WriteStartArray();
            context.Writer.WriteObjectId(value.Id);
            context.Writer.WriteString(value.City);
            context.Writer.WriteEndArray();
        }
    }
}

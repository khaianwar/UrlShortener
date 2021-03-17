using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Repository.Model
{
    public class UriModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Uri { get; set; }

        public string ShortUri { get; set; }
    }
}

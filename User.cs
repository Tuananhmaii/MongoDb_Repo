using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace MongoDb_Repo
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public DateTime? LastActivityTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public DateTime? CreationTime { get; set; }
    }
}

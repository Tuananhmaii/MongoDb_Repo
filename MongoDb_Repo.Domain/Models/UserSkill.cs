using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb_Repo.Domain.Models
{
    public record UserSkill
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get;} = ObjectId.GenerateNewId().ToString();
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AuthorId { get; init; }
        public DateTime Created { get; init; }  
        public DateTime Modified { get;} = DateTime.UtcNow;
        public string? SkillName { get; set; }
        public SkillLevel SkillLevel { get; set; } = SkillLevel.Unknown;
        public List<SkillProperties>? SkillProperties { get; set; }
        
    }

    public enum SkillLevel
    {
        Unknown,
        Beginner,
        Intermediate,
        Advanced,
        Expert
    }
}

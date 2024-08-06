using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDb_Repo.Domain.Models
{
    public record UserSkill
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? AuthorId { get; init; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Created { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Modified { get; } = DateTime.UtcNow;
        public string? SkillName { get; set; }
        public SkillLevel SkillLevel { get; set; } = SkillLevel.Unknown;
        public List<SkillProperty>? SkillProperties { get; set; }
        public string? FromFile { get; init; }

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

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MongoDb_Repo.Domain.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [Required(ErrorMessage = "ID is required")]
        public string Id { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string? Title { get; set; }
        public DateTime? LastActivityTime { get; set; }
        public DateTime? LastUpdateTime { get; set; }
        public DateTime? CreationTime { get; set; }
        public List<UserSkill>? UserSkills { get; set; }

    }
}

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDb_Repo.Domain.Models
{
    public class EvaluationFile
    {
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UploadedBy { get; set; }
        
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime UploadedTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime LastModified { get; set; }
        public EvaluationProperties[]? Data { get; set; }
    }

    public class EvaluationProperties
    {
        public string? Category { get; set; }
        public List<EvaluationPropertySubitems>? Subitems { get; set; }
    }

    public class EvaluationPropertySubitems
    {
        public string? Description { get; set; }
        public List<KeyValuePair<string, string>>? Items { get; set; }
    }
}
 
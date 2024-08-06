using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDb_Repo.Domain.Models;

namespace MongoDb_Repo.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<UserSkill> UserSkills => _database.GetCollection<UserSkill>("UserSkills");
        public IMongoCollection<SkillProperty> SkillProperties => _database.GetCollection<SkillProperty>("SkillProperties");
        public IMongoCollection<EvaluationFile> EvaluationFiles => _database.GetCollection<EvaluationFile>("EvaluationFiles");
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}

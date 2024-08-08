using MongoDB.Driver;
using MongoDb_Repo.Domain.Models;
using MongoDb_Repo.Domain.Interface.Repository;

namespace MongoDb_Repo.Domain.Repository
{
    public class SkillPropertiesRepository(IMongoDatabase database) : Repository<SkillProperty>(database,"SkillProperties"),ISkillPropertiesRepository
    {
    }
}

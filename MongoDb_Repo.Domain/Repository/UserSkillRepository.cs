using MongoDB.Driver;
using MongoDb_Repo.Domain.Models;
using MongoDb_Repo.Domain.Interface;

namespace MongoDb_Repo.Domain.Repository
{
    public class UserSkillRepository(IMongoDatabase database) : Repository<UserSkill>(database,"UserSkills"), IUserSkillRepository
    {
    }
}

using MongoDB.Driver;
using MongoDb_Repo.Domain.Models;
using MongoDb_Repo.Domain.Interface.Repository;

namespace MongoDb_Repo.Domain.Repository
{
    public class UserSkillRepository(IMongoDatabase database,ISkillPropertiesRepository skillPropertiesRepository) : Repository<UserSkill>(database,"UserSkills"), IUserSkillRepository
    {
        private readonly ISkillPropertiesRepository _skillPropertiesRepository = skillPropertiesRepository;
        public async Task<List<UserSkill>> AggregateWithProperties()
        {
            var collection = base._collection.AsQueryable();
            var results = from skills in collection
                          join properties in _skillPropertiesRepository.GetQueryAble() on skills.Id equals properties.SkillId into joined
                          select new UserSkill
                          {
                             Id = skills.Id,
                             AuthorId = skills.AuthorId,
                             Created = skills.Created,
                             Modified = skills.Modified,
                             SkillName = skills.SkillName,
                             SkillLevel = skills.SkillLevel,
                             FromFile = skills.FromFile,
                             SkillProperties = joined.ToList()
                          };
                          
            return results.ToList();
        }
    }
}

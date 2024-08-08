using MongoDb_Repo.Domain.Models;

namespace MongoDb_Repo.Domain.Interface.Repository
{
    public interface IUserSkillRepository : IRepository<UserSkill> 
    {
        Task<List<UserSkill>> AggregateWithProperties();
    }
}

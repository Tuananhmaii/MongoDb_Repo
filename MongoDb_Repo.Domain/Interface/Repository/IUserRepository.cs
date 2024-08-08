using MongoDb_Repo.Domain.Models;

namespace MongoDb_Repo.Domain.Interface.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}

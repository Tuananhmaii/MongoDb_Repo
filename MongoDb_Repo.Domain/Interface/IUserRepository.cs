using MongoDb_Repo.Domain.Models;

namespace MongoDb_Repo.Domain.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetUserList();
        Task<User> GetUserByEmail(string email);
    }
}

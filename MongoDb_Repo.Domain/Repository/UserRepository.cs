using MongoDB.Driver;
using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Models;

namespace MongoDb_Repo.Domain.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase database)
            : base(database, "Users")
        {
        }
        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

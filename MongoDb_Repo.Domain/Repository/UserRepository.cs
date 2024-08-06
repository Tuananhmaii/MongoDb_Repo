using MongoDB.Driver;
using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDb_Repo.Domain.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserSkillRepository _userSkillRepository;
        public UserRepository(IMongoDatabase database)
            : base(database, "Users")
        {
            _userSkillRepository = new UserSkillRepository(database);
        }

        public async Task<List<User>> GetUserList()
        {
            var filter = Builders<User>.Filter.Empty;
            var users = await _collection.Find(filter).ToListAsync();
            
            return users;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Email, email);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}

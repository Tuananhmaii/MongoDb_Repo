using MongoDB.Driver;
using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb_Repo.Domain.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IMongoDatabase database)
            : base(database, "Users")
        {
        }
    }
}

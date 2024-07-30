using MongoDb_Repo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDb_Repo.Domain.Interface
{
    public interface IUserRepository : IRepository<User>
    {
    }
}

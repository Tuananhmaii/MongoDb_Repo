using MongoDB.Driver;
using MongoDb_Repo.Domain.Interface;
using MongoDb_Repo.Domain.Models;

namespace MongoDb_Repo.Domain.Repository
{
    public class EvaluationFileRepository(IMongoDatabase database) : Repository<EvaluationFile>(database, "EvaluationFiles"),IEvaluationFileRepository{}
}

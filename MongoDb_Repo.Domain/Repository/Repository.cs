using MongoDB.Bson;
using MongoDB.Driver;
using MongoDb_Repo.Domain.Interface.Repository;
using System.Linq.Expressions;

namespace MongoDb_Repo.Domain.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public readonly IMongoCollection<T> _collection;

        public Repository(IMongoDatabase database, string collectionName)
        {
            _collection = database.GetCollection<T>(collectionName);
        }

        public async Task<T> GetAsync(string id)
        {
            return await _collection.Find(Builders<T>.Filter.Eq("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _collection.Find(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(string id, T entity)
        {
            await _collection.ReplaceOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)), entity);
        }

        public async Task RemoveAsync(string id)
        {
            await _collection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", new ObjectId(id)));
        }

        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            await _collection.InsertManyAsync(entities);
        }

        public IQueryable<T> GetQueryAble()
        {
            return _collection.AsQueryable();
        }
    }
}

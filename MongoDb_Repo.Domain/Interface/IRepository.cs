using System.Linq.Expressions;
namespace MongoDb_Repo.Domain.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task AddManyAsync(IEnumerable<T> entities);
        Task UpdateAsync(string id, T entity);
        Task RemoveAsync(string id);
    }
}

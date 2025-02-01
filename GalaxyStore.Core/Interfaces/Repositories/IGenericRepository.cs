
using System.Linq.Expressions;


namespace GalaxyStore.Core.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task<IEnumerable<T>> GetAllWithIncludesAsync(params Expression<Func<T, object>>[] includes);
        Task<T> GetByIdWithIncludesAsync(object id, params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        void Remove(T entity);
        Task<int> CountAsync(Expression<Func<T, bool>> predicate = null);
    }
}

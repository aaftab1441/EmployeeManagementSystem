
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
        Task InsertAsync(T entity);
        void Update(T entity);
        void Delete(Expression<Func<T, bool>> filter);
        Task SaveAsync();
    }
}
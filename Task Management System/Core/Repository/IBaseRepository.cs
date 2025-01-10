
using System.Linq.Expressions;


namespace MyTask_Management_System.Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GellAllAsync();
        Task<IEnumerable<T>> GellAllAsync(params Expression<Func<T, object>>[] includeProperty);

        Task<T> GetByIDAsync(int id);

        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}

using System.Linq.Expressions;

namespace PhotographySite.Data.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> ByIdAsync(object id);
        Task<IEnumerable<T>> AllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}

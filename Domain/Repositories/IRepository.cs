using System.Linq.Expressions;

namespace Domain.IRepositories;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
    Task UpdateAsync(T entity);
    void Update(T entity);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void UpdateRange(IEnumerable<T> entities);
    Task RemoveByIdAsync(int id);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> expression);
}

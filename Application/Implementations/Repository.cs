using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Domain.IRepositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDBContext _context;

    public Repository(ApplicationDBContext context)
    {
        _context = context;
    }

    public virtual async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public virtual void UpdateRange(IEnumerable<T> entities)
    {
        _context.Set<T>().UpdateRange(entities);
        _context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().Where(expression).ToListAsync();
    }

    public virtual Task<T> FindFirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression).FirstOrDefaultAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }


    public async Task RemoveByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);

        Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        _context.SaveChangesAsync();
    }

    public virtual  async Task UpdateAsync(T entity)
    {
         _context.Set<T>().Update(entity);
         await _context.SaveChangesAsync();
    }

    public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().AnyAsync(expression);
    }
}

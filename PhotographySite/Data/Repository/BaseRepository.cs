using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using System.Linq.Expressions;

namespace PhotographySite.Data.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly PhotographySiteDbContext _context;

    public BaseRepository(PhotographySiteDbContext context)
    {
        _context = context; 
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
    {
        return await _context.Set<T>().Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<T>> AllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> ByIdAsync(object id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    } 
} 
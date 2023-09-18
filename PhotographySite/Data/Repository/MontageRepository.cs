using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;
using System.Linq.Expressions;

namespace PhotographySite.Data.Repository;

public class MontageRepository : IMontageRepository
{
    private readonly PhotographySiteDbContext _context;

    public MontageRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Montage>> AllSortedAsync()
    {
        return await _context.Montage.OrderBy(montage => montage.Column).ThenBy(montage => montage.Order).ToListAsync();
    }

    public async Task<IEnumerable<Montage>> FindAsync(Expression<Func<Montage, bool>> expression)
    {
        return await _context.Montage.Where(expression).ToListAsync();
    }

    public async Task<Montage> ByIdAsync(int id)
    {
        return await _context.Montage.FindAsync(id);
    }

    public async Task<Montage> AddAsync(Montage montage)
    {
        await _context.Montage.AddAsync(montage);
        return montage;
    }

    public void Update(Montage montage)
    {
        _context.Montage.Update(montage);
    }

    public void Delete(Montage montage)
    {
        _context.Montage.Remove(montage);
    }
}
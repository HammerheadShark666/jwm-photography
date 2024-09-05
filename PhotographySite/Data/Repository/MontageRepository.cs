using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;
using System.Linq.Expressions;

namespace PhotographySite.Data.Repository;

public class MontageRepository(PhotographySiteDbContext context) : IMontageRepository
{
    public async Task<List<Montage>> AllSortedAsync()
    {
        return await context.Montage.OrderBy(montage => montage.Column).ThenBy(montage => montage.Order).ToListAsync();
    }

    public async Task<IEnumerable<Montage>> FindAsync(Expression<Func<Montage, bool>> expression)
    {
        return await context.Montage.Where(expression).ToListAsync();
    }

    public async Task<Montage> ByIdAsync(int id)
    {
        return await context.Montage.FindAsync(id);
    }

    public async Task<Montage> AddAsync(Montage montage)
    {
        await context.Montage.AddAsync(montage);
        return montage;
    }

    public void Update(Montage montage)
    {
        context.Montage.Update(montage);
    }

    public void Delete(Montage montage)
    {
        context.Montage.Remove(montage);
    }
}
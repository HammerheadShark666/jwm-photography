using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly PhotographySiteDbContext _context;

    public CategoryRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Category>> AllSortedAsync()
    {
        return await _context.Category.OrderBy(category => category.Name).ToListAsync();
    }

    public async Task<Category> ByIdAsync(int id)
    {
        return await _context.Category.FindAsync(id);
    }
}
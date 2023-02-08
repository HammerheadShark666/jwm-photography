using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(PhotographySiteDbContext context) : base(context) { }

    public async Task<List<Category>> AllSortedAsync()
    {
        return await _context.Category.OrderBy(c => c.Name).ToListAsync();
    }
}
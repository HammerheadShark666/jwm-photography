using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class CategoryRepository(PhotographySiteDbContext context) : ICategoryRepository
{
    public async Task<List<Category>> AllSortedAsync()
    {
        return await context.Category.OrderBy(category => category.Name).ToListAsync();
    }

    public async Task<Category> ByIdAsync(int id)
    {
        return await context.Category.FindAsync(id);
    }
}
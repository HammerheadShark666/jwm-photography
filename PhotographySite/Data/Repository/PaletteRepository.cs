using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class PaletteRepository(PhotographySiteDbContext context) : IPaletteRepository
{
    public async Task<List<Palette>> AllSortedAsync()
    {
        return await context.Palette.OrderBy(palette => palette.Name).ToListAsync();
    }

    public async Task<Palette> ByIdAsync(int id)
    {
        return await context.Palette.FindAsync(id);
    }
}
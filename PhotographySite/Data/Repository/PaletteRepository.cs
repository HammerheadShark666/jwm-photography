using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class PaletteRepository : IPaletteRepository
{
    private readonly PhotographySiteDbContext _context;

    public PaletteRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Palette>> AllSortedAsync()
    {
        return await _context.Palette.OrderBy(palette => palette.Name).ToListAsync();
    }

    public async Task<Palette> ByIdAsync(int id)
    {
        return await _context.Palette.FindAsync(id);
    }
}
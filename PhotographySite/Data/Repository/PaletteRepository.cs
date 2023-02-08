using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository
{
    public class PaletteRepository : BaseRepository<Palette>, IPaletteRepository
    {
        public PaletteRepository(PhotographySiteDbContext context) : base(context) { }

        public async Task<List<Palette>> AllSortedAsync()
        {
            return await _context.Palette.OrderBy(c => c.Name).ToListAsync();
        }
    }
}
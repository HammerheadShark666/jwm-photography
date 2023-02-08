using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class GalleryRepository : BaseRepository<Gallery>, IGalleryRepository
{
    public GalleryRepository(PhotographySiteDbContext context) : base(context) { }

    public async Task<List<Gallery>> AllSortedAsync()
    {
        return await _context.Gallery.OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _context.Gallery
                                .Where(a => a.Name.Equals(name)).AnyAsync();
    }

    public async Task<bool> ExistsAsync(long id, string name)
    {
        return await _context.Gallery
                               .Where(a => a.Name.Equals(name)
                                    && !a.Id.Equals(id))
                               .AnyAsync();
    }

    public async Task<Gallery> GetFullGalleryAsync(long id)
    {
        Gallery gallery = await _context.Gallery
                .Include(i => i.Photos)
                .ThenInclude(s => s.Photo)
                .ThenInclude(p => p.Country) 
                .SingleAsync(x => x.Id == id);

        return gallery;
    }
}
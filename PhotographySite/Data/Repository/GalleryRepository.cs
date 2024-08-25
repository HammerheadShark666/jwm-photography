using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class GalleryRepository : IGalleryRepository
{
    private readonly PhotographySiteDbContext _context;

    public GalleryRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Gallery>> AllSortedAsync()
    {
        return await _context.Gallery.OrderBy(gallery => gallery.Name).ToListAsync();
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _context.Gallery
                                .Where(gallery => gallery.Name.Equals(name)).AnyAsync();
    }

    public async Task<bool> ExistsAsync(long id, string name)
    {
        return await _context.Gallery
                               .Where(gallery => gallery.Name.Equals(name)
                                    && !gallery.Id.Equals(id))
                               .AnyAsync();
    }

    public async Task<Gallery> GetFullGalleryAsync(long id)
    {
        return await _context.Gallery
                .Include(gallery => gallery.Photos)
                .ThenInclude(photos => photos.Photo)
                .ThenInclude(photo => photo.Country)
                .SingleAsync(gallery => gallery.Id == id);
    }

    public async Task<Gallery> GetFullGalleryAsync(Guid userId, long id)
    {
        return await _context.Gallery
            .Include(gallery => gallery.Photos)
                .ThenInclude(photo => photo.Photo.Country)
            .Include(gallery => gallery.Photos)
                .ThenInclude(photo => photo.Photo.Favourites.Where(favourite => favourite.UserId == userId))

            .SingleAsync(gallery => gallery.Id == id);
    }

    public async Task AddAsync(Gallery gallery)
    {
        await _context.Gallery.AddAsync(gallery);
    }

    public void Update(Gallery gallery)
    {
        _context.Gallery.Update(gallery);
    }

    public void Delete(Gallery gallery)
    {
        _context.Gallery.Remove(gallery);
    }

    public async Task<Gallery> ByIdAsync(long id)
    {
        return await _context.Gallery.FindAsync(id);
    }
}
using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class GalleryRepository(PhotographySiteDbContext context) : IGalleryRepository
{
    public async Task<List<Gallery>> AllSortedAsync()
    {
        return await context.Gallery.OrderBy(gallery => gallery.Name).ToListAsync();
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await context.Gallery
                                .Where(gallery => gallery.Name.Equals(name)).AnyAsync();
    }

    public async Task<bool> ExistsAsync(long id, string name)
    {
        return await context.Gallery
                               .Where(gallery => gallery.Name.Equals(name)
                                    && !gallery.Id.Equals(id))
                               .AnyAsync();
    }

    public async Task<Gallery> GetFullGalleryAsync(long id)
    {
        return await context.Gallery
                .Include(gallery => gallery.Photos)
                .ThenInclude(photos => photos.Photo)
                .ThenInclude(photo => photo.Country)
                .SingleAsync(gallery => gallery.Id == id);
    }

    public async Task<Gallery> GetFullGalleryAsync(Guid userId, long id)
    {
        return await context.Gallery
            .Include(gallery => gallery.Photos)
                .ThenInclude(photo => photo.Photo.Country)
            .Include(gallery => gallery.Photos)
                .ThenInclude(photo => photo.Photo.Favourites.Where(favourite => favourite.UserId == userId))

            .SingleAsync(gallery => gallery.Id == id);
    }

    public async Task AddAsync(Gallery gallery)
    {
        await context.Gallery.AddAsync(gallery);
    }

    public void Update(Gallery gallery)
    {
        context.Gallery.Update(gallery);
    }

    public void Delete(Gallery gallery)
    {
        context.Gallery.Remove(gallery);
    }

    public async Task<Gallery> ByIdAsync(long id)
    {
        return await context.Gallery.FindAsync(id);
    }
}
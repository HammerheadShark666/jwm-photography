using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class UserGalleryRepository : IUserGalleryRepository
{

    private readonly PhotographySiteDbContext _context;

    public UserGalleryRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<UserGallery>> AllSortedAsync()
    {
        return await _context.UserGallery.OrderBy(userGallery => userGallery.Name).ToListAsync();
    }

    public async Task<List<UserGallery>> AllSortedAsync(Guid userId)
    {
        return await _context.UserGallery.Where(userGallery => userGallery.UserId == userId).OrderBy(userGallery => userGallery.Name).ToListAsync();
    }

    public async Task<UserGallery> GetAsync(Guid userId, long galleryId)
    {
        return await _context.UserGallery.Where(userGallery => userGallery.UserId == userId && userGallery.Id == galleryId).SingleOrDefaultAsync();
    }

    public async Task<bool> ExistsAsync(Guid userId, string name)
    {
        return await _context.UserGallery
                                .Where(userGallery => userGallery.UserId == userId && userGallery.Name.Equals(name)).AnyAsync();
    }

    public async Task<bool> ExistsAsync(Guid userId, long id, string name)
    {
        return await _context.UserGallery
                               .Where(userGallery => userGallery.Name.Equals(name)
                                    && !userGallery.Id.Equals(id) && userGallery.UserId == userId)
                               .AnyAsync();
    }

    public async Task<UserGallery> GetFullGalleryAsync(Guid userId, long id)
    {
        return await _context.UserGallery
                                .Include(userGallery => userGallery.Photos)
                                .ThenInclude(photos => photos.Photo)
                                .ThenInclude(photo => photo.Country)
                                .SingleOrDefaultAsync(userGallery => userGallery.UserId == userId && userGallery.Id == id);
    }

    public async Task AddAsync(UserGallery gallery)
    {
        await _context.UserGallery.AddAsync(gallery);
    }

    public void Update(UserGallery userGallery)
    {
        _context.UserGallery.Update(userGallery);
    }

    public void Delete(UserGallery userGallery)
    {
        _context.UserGallery.Remove(userGallery);
    }

    public async Task<UserGallery> ByIdAsync(long id)
    {
        return await _context.UserGallery.FindAsync(id);
    }
}
using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class FavouriteRepository : BaseRepository<Favourite>, IFavouriteRepository
{
    public FavouriteRepository(PhotographySiteDbContext context) : base(context) { }

    public async Task<List<Photo>> GetFavouritePhotosAsync(Guid userId)
    {
        return await (from favourite in _context.Favourite
                      join photo in _context.Photo                        
                            on favourite.PhotoId equals photo.Id 
                      where favourite.UserId == userId 
                      select photo).ToListAsync();
    }

    public async Task<Favourite> GetFavouritePhotoAsync(Guid userId, long photoId)
    {
        return await (from favourite in _context.Favourite                     
                      where favourite.UserId == userId && favourite.PhotoId == photoId
                      select favourite).SingleOrDefaultAsync();
    }
}
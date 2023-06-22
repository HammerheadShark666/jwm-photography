using LinqKit;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Areas.Admin.Dtos;
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

    public async Task<List<Photo>> GetFavouritePhotoByPagingAsync(Guid userId, PhotoFilterDto photoFilterDto)
    {  
            return await _context.Favourite
                .Include(favourite => favourite.Photo)
                .ThenInclude(photo => photo.Country)
                .Include(photo => photo.Photo.Category)
                .Include(photo => photo.Photo.Palette)
                .Where(GetPredicateWhereClause(userId, photoFilterDto)) 
                .OrderBy(x => x.Photo.Title)
                .Skip((photoFilterDto.PageIndex - 1) * photoFilterDto.PageSize).Take(photoFilterDto.PageSize)
                .Select(favourite => favourite.Photo)
                .ToListAsync();     
    }

    public async Task<int> ByFilterCountAsync(Guid userId, PhotoFilterDto photoFilterDto)
    {
        return await _context.Favourite
           .Where(GetPredicateWhereClause(userId, photoFilterDto))
           .CountAsync();
    } 

    private ExpressionStarter<Favourite> GetPredicateWhereClause(Guid userId, PhotoFilterDto photoFilterDto)
    {
        var predicate = PredicateBuilder.New<Favourite>(true);

        predicate = predicate.And(favourite => favourite.UserId.Equals(userId));

        if (photoFilterDto.Id > 0)
            predicate = predicate.And(favourite => favourite.Photo.Id.Equals(photoFilterDto.Id));

        if (photoFilterDto.Iso > 0)
            predicate = predicate.And(favourite => favourite.Photo.Iso.Equals(photoFilterDto.Iso));

        if (photoFilterDto.PaletteId > 0)
            predicate = predicate.And(favourite => favourite.Photo.Palette.Id.Equals(photoFilterDto.PaletteId));

        if (photoFilterDto.CategoryId > 0)
            predicate = predicate.And(favourite => favourite.Photo.Category.Id.Equals(photoFilterDto.CategoryId));

        if (photoFilterDto.CountryId > 0)
            predicate = predicate.And(favourite => favourite.Photo.Country.Id.Equals(photoFilterDto.CountryId));

        if (photoFilterDto.DateTaken != null)
            predicate = predicate.And(favourite => favourite.Photo.DateTaken.Equals(photoFilterDto.DateTaken));

        if (!string.IsNullOrEmpty(photoFilterDto.FileName))
            predicate = predicate.And(favourite => favourite.Photo.FileName.Contains(photoFilterDto.FileName));

        if (!string.IsNullOrEmpty(photoFilterDto.FocalLength))
            predicate = predicate.And(favourite => favourite.Photo.FocalLength.Contains(photoFilterDto.FocalLength));

        if (!string.IsNullOrEmpty(photoFilterDto.Lens))
            predicate = predicate.And(favourite => favourite.Photo.Lens.Contains(photoFilterDto.Lens));

        if (!string.IsNullOrEmpty(photoFilterDto.AperturValue))
            predicate = predicate.And(favourite => favourite.Photo.AperturValue.Contains(photoFilterDto.AperturValue));

        if (!string.IsNullOrEmpty(photoFilterDto.Camera))
            predicate = predicate.And(favourite => favourite.Photo.Camera.Contains(photoFilterDto.Camera));

        if (!string.IsNullOrEmpty(photoFilterDto.Title))
            predicate = predicate.And(favourite => favourite.Photo.Title.Contains(photoFilterDto.Title));

        if (!string.IsNullOrEmpty(photoFilterDto.ExposureTime))
            predicate = predicate.And(favourite => favourite.Photo.ExposureTime.Contains(photoFilterDto.ExposureTime));

        return predicate;
    }
}
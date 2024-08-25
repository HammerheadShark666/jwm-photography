using LinqKit;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class FavouriteRepository : IFavouriteRepository
{
    private readonly PhotographySiteDbContext _context;

    public FavouriteRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

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
                      where favourite.UserId == userId
                        && favourite.PhotoId == photoId
                      select favourite).SingleOrDefaultAsync();
    }

    public async Task<Boolean> PhotoIsAlreadyAFavourite(Favourite favourite)
    {
        return await GetFavouritePhotoAsync(favourite.UserId, favourite.PhotoId) != null;
    }

    public async Task<List<Photo>> GetFavouritePhotoByPagingAsync(Guid userId, PhotoFilterRequest photoFilterRequest)
    {
        return await _context.Favourite
            .Include(favourite => favourite.Photo)
            .ThenInclude(photo => photo.Country)
            .Include(photo => photo.Photo.Category)
            .Include(photo => photo.Photo.Palette)
            .Where(GetPredicateWhereClause(userId, photoFilterRequest))
            .OrderBy(x => x.Photo.Title)
            .Skip((photoFilterRequest.PageIndex - 1) * photoFilterRequest.PageSize).Take(photoFilterRequest.PageSize)
            .Select(favourite => favourite.Photo)
            .ToListAsync();
    }

    public async Task<int> ByFilterCountAsync(Guid userId, PhotoFilterRequest photoFilterRequest)
    {
        return await _context.Favourite
           .Where(GetPredicateWhereClause(userId, photoFilterRequest))
           .CountAsync();
    }

    private ExpressionStarter<Favourite> GetPredicateWhereClause(Guid userId, PhotoFilterRequest photoFilterRequest)
    {
        var predicate = PredicateBuilder.New<Favourite>(true);

        predicate = predicate.And(favourite => favourite.UserId.Equals(userId));

        if (photoFilterRequest.Id > 0)
            predicate = predicate.And(favourite => favourite.Photo.Id.Equals(photoFilterRequest.Id));

        if (photoFilterRequest.Iso > 0)
            predicate = predicate.And(favourite => favourite.Photo.Iso.Equals(photoFilterRequest.Iso));

        if (photoFilterRequest.PaletteId > 0)
            predicate = predicate.And(favourite => favourite.Photo.Palette.Id.Equals(photoFilterRequest.PaletteId));

        if (photoFilterRequest.CategoryId > 0)
            predicate = predicate.And(favourite => favourite.Photo.Category.Id.Equals(photoFilterRequest.CategoryId));

        if (photoFilterRequest.CountryId > 0)
            predicate = predicate.And(favourite => favourite.Photo.Country.Id.Equals(photoFilterRequest.CountryId));

        if (photoFilterRequest.DateTaken != null)
            predicate = predicate.And(favourite => favourite.Photo.DateTaken.Equals(photoFilterRequest.DateTaken));

        if (!string.IsNullOrEmpty(photoFilterRequest.FileName))
            predicate = predicate.And(favourite => favourite.Photo.FileName.Contains(photoFilterRequest.FileName));

        if (!string.IsNullOrEmpty(photoFilterRequest.FocalLength))
            predicate = predicate.And(favourite => favourite.Photo.FocalLength.Contains(photoFilterRequest.FocalLength));

        if (!string.IsNullOrEmpty(photoFilterRequest.Lens))
            predicate = predicate.And(favourite => favourite.Photo.Lens.Contains(photoFilterRequest.Lens));

        if (!string.IsNullOrEmpty(photoFilterRequest.AperturValue))
            predicate = predicate.And(favourite => favourite.Photo.AperturValue.Contains(photoFilterRequest.AperturValue));

        if (!string.IsNullOrEmpty(photoFilterRequest.Camera))
            predicate = predicate.And(favourite => favourite.Photo.Camera.Contains(photoFilterRequest.Camera));

        if (!string.IsNullOrEmpty(photoFilterRequest.Title))
            predicate = predicate.And(favourite => favourite.Photo.Title.Contains(photoFilterRequest.Title));

        if (!string.IsNullOrEmpty(photoFilterRequest.ExposureTime))
            predicate = predicate.And(favourite => favourite.Photo.ExposureTime.Contains(photoFilterRequest.ExposureTime));

        return predicate;
    }
    public void Delete(Favourite favourite)
    {
        _context.Favourite.Remove(favourite);
    }

    public async Task AddAsync(Favourite favourite)
    {
        await _context.Favourite.AddAsync(favourite);
    }
}
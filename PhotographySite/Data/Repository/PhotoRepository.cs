using LinqKit;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;
using System.Linq.Dynamic.Core;

namespace PhotographySite.Data.Repository;

public class PhotoRepository : IPhotoRepository
{
    private readonly PhotographySiteDbContext _context;

    public PhotoRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Photo>> AllAsync()
    {
        return await _context.Photo
            .Include(photo => photo.Country) 
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Photo.CountAsync();                 
    }

    public async Task<List<Photo>> ByPagingAsync(PhotoFilterRequest photoFilterRequest)
    {
        return await _context.Photo
            .Include(photo => photo.Country)
            .Include(photo => photo.Category)
            .Include(photo => photo.Palette)
            .Where(GetPredicateWhereClause(photoFilterRequest))
            .OrderBy(GetOrderByStatement(photoFilterRequest))
            .Skip((photoFilterRequest.PageIndex - 1) * photoFilterRequest.PageSize).Take(photoFilterRequest.PageSize)
            .ToListAsync();
    }

    public async Task<int> ByFilterCountAsync(PhotoFilterRequest photoFilterRequest)
    {
        return await _context.Photo                 
           .Where(GetPredicateWhereClause(photoFilterRequest))
           .CountAsync(); 
    }

    public List<Photo> MontagePhotos(Helpers.Enums.PhotoOrientation orientation, int numberOfPhotos, Guid userId)
    {
        return _context.Photo
            .Include(photo => photo.Country)
            .Include(photo => photo.Favourites.Where(favourite => favourite.UserId == userId))
            .Where(p => p.UseInMontage == true && (int)orientation == p.Orientation)
            .Distinct()
            .OrderBy(x => Guid.NewGuid())
            .Take(numberOfPhotos)
            .ToList();
    }

    public bool Exists(string filename)
    {
        return _context.Photo.Count(photo => photo.FileName == filename) > 0; 
    }

    public async Task<Photo> FindByFilenameAsync(string filename)
    {
        return await _context.Photo.FirstOrDefaultAsync(photo => photo.FileName == filename);
    }

    public async Task<List<Photo>> GetLatestPhotos(int numberOfPhotos)
    { 
        return await (from photo in _context.Photo
                      orderby photo.Id descending
                      select photo)
                      .Take(numberOfPhotos)
                      .ToListAsync(); 
    }

    private string GetOrderByStatement(PhotoFilterRequest photoFilterRequest)
    {
        string orderByStatement = "FileName asc";

        if (!string.IsNullOrEmpty(photoFilterRequest.SortField) && !string.IsNullOrEmpty(photoFilterRequest.SortOrder))
            orderByStatement = photoFilterRequest.SortField + " " + photoFilterRequest.SortOrder;

        return orderByStatement;
    }

    private ExpressionStarter<Photo> GetPredicateWhereClause(PhotoFilterRequest photoFilterRequest)
    {
        var predicate = PredicateBuilder.New<Photo>(true);

        if (photoFilterRequest.Id > 0)
            predicate = predicate.And(photo => photo.Id.Equals(photoFilterRequest.Id));

        if (photoFilterRequest.Iso > 0)
            predicate = predicate.And(photo => photo.Iso.Equals(photoFilterRequest.Iso));

        if (photoFilterRequest.PaletteId > 0)
            predicate = predicate.And(photo => photo.Palette.Id.Equals(photoFilterRequest.PaletteId));

        if (photoFilterRequest.CategoryId > 0)
            predicate = predicate.And(photo => photo.Category.Id.Equals(photoFilterRequest.CategoryId));

        if (photoFilterRequest.CountryId > 0)
            predicate = predicate.And(photo => photo.Country.Id.Equals(photoFilterRequest.CountryId));

        if (photoFilterRequest.DateTaken != null)
            predicate = predicate.And(photo => photo.DateTaken.Equals(photoFilterRequest.DateTaken));

        if (!string.IsNullOrEmpty(photoFilterRequest.FileName))
            predicate = predicate.And(photo => photo.FileName.Contains(photoFilterRequest.FileName));

        if (!string.IsNullOrEmpty(photoFilterRequest.FocalLength))
            predicate = predicate.And(photo => photo.FocalLength.Contains(photoFilterRequest.FocalLength));

        if (!string.IsNullOrEmpty(photoFilterRequest.Lens))
            predicate = predicate.And(photo => photo.Lens.Contains(photoFilterRequest.Lens));

        if (!string.IsNullOrEmpty(photoFilterRequest.AperturValue))
            predicate = predicate.And(photo => photo.AperturValue.Contains(photoFilterRequest.AperturValue));

        if (!string.IsNullOrEmpty(photoFilterRequest.Camera))
            predicate = predicate.And(photo => photo.Camera.Contains(photoFilterRequest.Camera));

        if (!string.IsNullOrEmpty(photoFilterRequest.Title))
            predicate = predicate.And(photo => photo.Title.Contains(photoFilterRequest.Title));

        if (!string.IsNullOrEmpty(photoFilterRequest.ExposureTime))
            predicate = predicate.And(photo => photo.ExposureTime.Contains(photoFilterRequest.ExposureTime));

        return predicate;
    }

    public async Task AddAsync(Photo photo)
    {
        await _context.Photo.AddAsync(photo);
    }

    public void Update(Photo photo)
    {
        _context.Photo.Update(photo);
    }

    public async Task<Photo> ByIdAsync(long id)
    {
        return await _context.Photo.FindAsync(id);
    }
}
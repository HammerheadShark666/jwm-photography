using LinqKit;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;
using PhotographySite.Models.Dto;
using System.Linq.Dynamic.Core;

namespace PhotographySite.Data.Repository;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
{
    public PhotoRepository(PhotographySiteDbContext context) : base(context) { }

    public new async Task<List<Photo>> AllAsync()
    {
        return await _context.Photo
            .Include(photo => photo.Country) 
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Photo.CountAsync();                 
    }

    public async Task<List<Photo>> ByPagingAsync(PhotoFilterDto photoFilterDto)
    { 
        return await _context.Photo
            .Include(photo => photo.Country)
            .Include(photo => photo.Category)
            .Include(photo => photo.Palette)
            .Where(GetPredicateWhereClause(photoFilterDto))
            .OrderBy(GetOrderByStatement(photoFilterDto))
            .Skip((photoFilterDto.PageIndex-1) * photoFilterDto.PageSize).Take(photoFilterDto.PageSize)
            .ToListAsync();
    }

    public async Task<int> ByFilterCountAsync(PhotoFilterDto photoFilterDto)
    {
        return await _context.Photo                 
           .Where(GetPredicateWhereClause(photoFilterDto))
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

    private string GetOrderByStatement(PhotoFilterDto photoFilterDto)
    {
        string orderByStatement = "FileName asc";

        if (!string.IsNullOrEmpty(photoFilterDto.SortField) && !string.IsNullOrEmpty(photoFilterDto.SortOrder))
            orderByStatement = photoFilterDto.SortField + " " + photoFilterDto.SortOrder;

        return orderByStatement;
    }

    private ExpressionStarter<Photo> GetPredicateWhereClause(PhotoFilterDto photoFilterDto)
    {
        var predicate = PredicateBuilder.New<Photo>(true);

        if (photoFilterDto.Id > 0)
            predicate = predicate.And(photo => photo.Id.Equals(photoFilterDto.Id));

        if (photoFilterDto.Iso > 0)
            predicate = predicate.And(photo => photo.Iso.Equals(photoFilterDto.Iso));

        if (photoFilterDto.PaletteId > 0)
            predicate = predicate.And(photo => photo.Palette.Id.Equals(photoFilterDto.PaletteId));

        if (photoFilterDto.CategoryId > 0)
            predicate = predicate.And(photo => photo.Category.Id.Equals(photoFilterDto.CategoryId));

        if (photoFilterDto.CountryId > 0)
            predicate = predicate.And(photo => photo.Country.Id.Equals(photoFilterDto.CountryId));

        if (photoFilterDto.DateTaken != null)
            predicate = predicate.And(photo => photo.DateTaken.Equals(photoFilterDto.DateTaken));

        if (!string.IsNullOrEmpty(photoFilterDto.FileName))
            predicate = predicate.And(photo => photo.FileName.Contains(photoFilterDto.FileName));

        if (!string.IsNullOrEmpty(photoFilterDto.FocalLength))
            predicate = predicate.And(photo => photo.FocalLength.Contains(photoFilterDto.FocalLength));

        if (!string.IsNullOrEmpty(photoFilterDto.Lens))
            predicate = predicate.And(photo => photo.Lens.Contains(photoFilterDto.Lens));

        if (!string.IsNullOrEmpty(photoFilterDto.AperturValue))
            predicate = predicate.And(photo => photo.AperturValue.Contains(photoFilterDto.AperturValue));

        if (!string.IsNullOrEmpty(photoFilterDto.Camera))
            predicate = predicate.And(photo => photo.Camera.Contains(photoFilterDto.Camera));

        if (!string.IsNullOrEmpty(photoFilterDto.Title))
            predicate = predicate.And(photo => photo.Title.Contains(photoFilterDto.Title));

        if (!string.IsNullOrEmpty(photoFilterDto.ExposureTime))
            predicate = predicate.And(photo => photo.ExposureTime.Contains(photoFilterDto.ExposureTime));

        return predicate;
    }
}
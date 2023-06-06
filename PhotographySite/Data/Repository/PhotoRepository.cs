using LinqKit;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;
using System.Linq.Dynamic.Core;

namespace PhotographySite.Data.Repository;

public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
{
    public PhotoRepository(PhotographySiteDbContext context) : base(context) { }

    public new async Task<List<Photo>> AllAsync()
    {
        return await _context.Photo
            .Include(a => a.Country) 
            .ToListAsync();
    }

    public async Task<int> CountAsync()
    {
        return await _context.Photo.CountAsync();                 
    }

    public async Task<List<Photo>> ByPagingAsync(PhotoFilterDto photoFilterDto)
    { 
        return await _context.Photo
            .Include(a => a.Country)
            .Include(a => a.Category)
            .Include(a => a.Palette)
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
            .Include(a => a.Country)
            .Include(a => a.Favourites.Where(favourite => favourite.UserId == userId))
            .Where(p => p.UseInMontage == true && (int)orientation == p.Orientation)
            .Distinct()
            .OrderBy(x => Guid.NewGuid())               
            .Take(numberOfPhotos)                
            .ToList();
    }

    public bool Exists(string filename)
    {
        return _context.Photo.Count(p => p.FileName == filename) > 0; 
    }

    public async Task<Photo> FindByFilenameAsync(string filename)
    {
        return await _context.Photo.FirstOrDefaultAsync(p => p.FileName == filename);
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
            predicate = predicate.And(p => p.Id.Equals(photoFilterDto.Id));

        if (photoFilterDto.Iso > 0)
            predicate = predicate.And(p => p.Iso.Equals(photoFilterDto.Iso));

        if (photoFilterDto.PaletteId > 0)
            predicate = predicate.And(p => p.Palette.Id.Equals(photoFilterDto.PaletteId));

        if (photoFilterDto.CategoryId > 0)
            predicate = predicate.And(p => p.Category.Id.Equals(photoFilterDto.CategoryId));

        if (photoFilterDto.CountryId > 0)
            predicate = predicate.And(p => p.Country.Id.Equals(photoFilterDto.CountryId));

        if (photoFilterDto.DateTaken != null)
            predicate = predicate.And(p => p.DateTaken.Equals(photoFilterDto.DateTaken));

        if (!string.IsNullOrEmpty(photoFilterDto.FileName))
            predicate = predicate.And(p => p.FileName.Contains(photoFilterDto.FileName));

        if (!string.IsNullOrEmpty(photoFilterDto.FocalLength))
            predicate = predicate.And(p => p.FocalLength.Contains(photoFilterDto.FocalLength));

        if (!string.IsNullOrEmpty(photoFilterDto.Lens))
            predicate = predicate.And(p => p.Lens.Contains(photoFilterDto.Lens));

        if (!string.IsNullOrEmpty(photoFilterDto.AperturValue))
            predicate = predicate.And(p => p.AperturValue.Contains(photoFilterDto.AperturValue));

        if (!string.IsNullOrEmpty(photoFilterDto.Camera))
            predicate = predicate.And(p => p.Camera.Contains(photoFilterDto.Camera));

        if (!string.IsNullOrEmpty(photoFilterDto.Title))
            predicate = predicate.And(p => p.Title.Contains(photoFilterDto.Title));

        if (!string.IsNullOrEmpty(photoFilterDto.ExposureTime))
            predicate = predicate.And(p => p.ExposureTime.Contains(photoFilterDto.ExposureTime));

        return predicate;
    }
}
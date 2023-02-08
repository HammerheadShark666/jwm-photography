using PhotographySite.Areas.Admin.Models;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IGalleryService
{
    Task<GalleryDto> GetGalleryAsync(long id);

    Task<List<GalleryDto>> GetGalleriesAsync();

    Task<SearchPhotosResultsDto> SearchPhotosAsync(SearchPhotosDto searchPhotosDto);

    Task<GalleryNameDto> SaveName(GalleryNameDto galleryNameDto);

    Task<GalleryNameDto> SaveNewGalleryAsync(GalleryNameDto galleryNameDto);

    Task DeleteAsync(long id);
}

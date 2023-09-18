using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IGalleryService
{
    Task<GalleryResponse> GetGalleryAsync(long id);
    Task<GalleryResponse> GetGalleryAsync(Guid userId, long id);
    Task<GalleriesResponse> GetGalleriesAsync(); 
}
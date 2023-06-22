using PhotographySite.Areas.Admin.Dtos; 

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IGalleryService
{
    Task<GalleryDto> GetGalleryAsync(Guid userId, long id);

    Task<GalleriesDto> GetGalleriesAsync(); 
}

using PhotographySite.Areas.Admin.Models; 

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IGalleryService
{
    Task<GalleryDto> GetGalleryAsync(long id);

    Task<GalleriesDto> GetGalleriesAsync(); 
}

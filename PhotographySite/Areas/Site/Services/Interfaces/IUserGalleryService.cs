using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IUserGalleryService
{
    Task<UserGalleryDto> GetUserGalleryAsync(Guid userId, long id);

    Task<List<UserGalleryDto>> GetUserGalleriesAsync(HttpContext httpContext);

    Task<SearchPhotosResultsDto> SearchPhotosAsync(SearchPhotosDto searchPhotosDto);

    Task<UserGalleryNameDto> SaveName(UserGalleryNameDto galleryNameDto);

    Task<UserGalleryNameDto> SaveNewUserGalleryAsync(UserGalleryNameDto galleryNameDto);

    Task DeleteAsync(long id);
}

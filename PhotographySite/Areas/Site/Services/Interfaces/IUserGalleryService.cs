using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IUserGalleryService
{
    Task<UserGalleryToEditResponse> GetUserGalleryToEditAsync(Guid userId, long id);
    Task<UserGalleryResponse> GetUserGalleryAsync(Guid userId, long id);
    Task<List<UserGalleryResponse>> GetUserGalleriesAsync(HttpContext httpContext); 
    Task<SearchPhotosResponse> SearchPhotosAsync(SearchPhotosRequest searchPhotosRequest);
    Task<UserGalleryActionResponse> AddAsync(UserGalleryAddRequest userGalleryAddRequest);
    Task<UserGalleryActionResponse> UpdateAsync(UserGalleryUpdateRequest galleryUpdateRequest);
    Task<UserGalleryActionResponse> DeleteAsync(Guid userId, long id);
}
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IGalleryService
{
    Task<GalleryResponse> GetGalleryAsync(long id);
    Task<List<GalleryResponse>> GetGalleriesAsync();
    Task<SearchPhotosResponse> SearchPhotosAsync(SearchPhotosRequest searchPhotosRequest);
    Task<GalleryActionResponse> AddAsync(GalleryUpdateRequest galleryAddRequest);
    Task<GalleryActionResponse> UpdateAsync(GalleryUpdateRequest galleryUpdateRequest);
    Task<GalleryActionResponse> DeleteAsync(int id);
}
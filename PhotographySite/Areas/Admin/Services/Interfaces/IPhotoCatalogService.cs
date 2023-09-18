using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IPhotoCatalogService
{
    Task<LookupsResponse> GetLookupsAsync(); 
    Task<PhotoPageResponse> GetPhotosPageAsync(PhotoFilterRequest photoFilterRequest);
    Task UpdatePhotoAsync(UpdatePhotoRequest updatePhotoRequest);
    Task<List<PhotoResponse>> GetLatestPhotos(int NumberOfPhotos);
}
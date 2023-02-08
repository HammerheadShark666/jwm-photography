using PhotographySite.Areas.Admin.Models;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IPhotoCatalogService
{
    Task<LookupsDto> GetLookupsAsync(); 
    Task<PhotosPageDto> GetPhotosPageAsync(PhotoFilterDto photoFilterDto);
    Task UpdatePhotoAsync(UpdatePhotoDto updatePhotoDto);
}

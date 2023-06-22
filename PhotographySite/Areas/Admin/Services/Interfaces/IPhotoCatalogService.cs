using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IPhotoCatalogService
{
    Task<LookupsDto> GetLookupsAsync(); 
    Task<PhotosPageDto> GetPhotosPageAsync(PhotoFilterDto photoFilterDto);
    Task UpdatePhotoAsync(UpdatePhotoDto updatePhotoDto);
    Task<List<PhotoDto>> GetLatestPhotos(int NumberOfPhotos);
}

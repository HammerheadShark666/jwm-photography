using AutoMapper;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Services;

public class PhotoCatalogService(IUnitOfWork unitOfWork, IMapper mapper) : IPhotoCatalogService
{
    public async Task<LookupsResponse> GetLookupsAsync()
    {
        return new LookupsResponse()
        {
            Categories = mapper.Map<List<CategoryResponse>>(await unitOfWork.Categories.AllSortedAsync()),
            Countries = mapper.Map<List<CountryResponse>>(await unitOfWork.Countries.AllSortedAsync()),
            Palettes = mapper.Map<List<PaletteResponse>>(await unitOfWork.Palettes.AllSortedAsync())
        };
    }

    public async Task<PhotoPageResponse> GetPhotosPageAsync(PhotoFilterRequest photoFilterRequest)
    {
        return new PhotoPageResponse()
        {
            Data = mapper.Map<List<PhotoResponse>>(await unitOfWork.Photos.ByPagingAsync(photoFilterRequest)),
            ItemsCount = await unitOfWork.Photos.ByFilterCountAsync(photoFilterRequest)
        };
    }

    public async Task UpdatePhotoAsync(UpdatePhotoRequest updatePhotoRequest)
    {

        var photo = await unitOfWork.Photos.ByIdAsync(updatePhotoRequest.Id);

        if (photo != null)
        {
            photo.Title = updatePhotoRequest.Title;
            photo.Country = await unitOfWork.Countries.ByIdAsync(updatePhotoRequest.CountryId);
            photo.Category = await unitOfWork.Categories.ByIdAsync(updatePhotoRequest.CategoryId);
            photo.Palette = await unitOfWork.Palettes.ByIdAsync(updatePhotoRequest.PaletteId);

            unitOfWork.Photos.Update(photo);
            await unitOfWork.Complete();
        }
    }

    public async Task<List<PhotoResponse>> GetLatestPhotos(int numberOfPhotos)
    {
        return mapper.Map<List<PhotoResponse>>(await unitOfWork.Photos.GetLatestPhotos(numberOfPhotos));
    }
}
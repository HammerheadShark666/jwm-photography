using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;
using PhotographySite.Helpers.Interface;
using PhotographySite.Models;
using SwanSong.Service.Helpers.Exceptions;

namespace PhotographySite.Areas.Admin.Services;

public class GalleryService(IUnitOfWork unitOfWork,
                      IMapper mapper,
                      IValidatorHelper<Gallery> validatorHelper,
                      IMemoryCache memoryCache) : IGalleryService
{
    public async Task<GalleryResponse> GetGalleryAsync(long id)
    {
        return mapper.Map<GalleryResponse>(await unitOfWork.Galleries.ByIdAsync(id));
    }

    public async Task<List<GalleryResponse>> GetGalleriesAsync()
    {
        return mapper.Map<List<GalleryResponse>>(await unitOfWork.Galleries.AllSortedAsync());
    }

    public async Task<SearchPhotosResponse> SearchPhotosAsync(SearchPhotosRequest searchPhotosRequest)
    {
        var photoFilterRequest = PhotoFilterRequest.Create(searchPhotosRequest);

        var photos = mapper.Map<List<PhotoResponse>>(await unitOfWork.Photos.ByPagingAsync(photoFilterRequest));
        int numberOfPhotos = await unitOfWork.Photos.ByFilterCountAsync(photoFilterRequest);
        int numberOfPages = GetNumberOfPages(numberOfPhotos, photoFilterRequest.PageSize);

        return new SearchPhotosResponse()
        {
            NumberOfPhotos = numberOfPhotos,
            PageIndex = searchPhotosRequest.PageIndex + 1,
            PageSize = searchPhotosRequest.PageSize,
            NumberOfPages = numberOfPages,
            Photos = photos
        };
    }

    public async Task<GalleryActionResponse> AddAsync(GalleryUpdateRequest galleryAddRequest)
    {
        var gallery = mapper.Map<Gallery>(galleryAddRequest);

        await validatorHelper.ValidateAsync(gallery, Constants.ValidationEventBeforeSave);
        await SaveAdd(gallery, CacheKeys.Gallery);

        return new GalleryActionResponse(gallery.Id, await validatorHelper.AfterEventAsync(gallery, Constants.ValidationEventAfterSave));
    }

    public async Task<GalleryActionResponse> UpdateAsync(GalleryUpdateRequest galleryUpdateRequest)
    {
        var gallery = await GetGallery(galleryUpdateRequest.Id);

        gallery.Name = galleryUpdateRequest.Name;

        await validatorHelper.ValidateAsync(gallery, Constants.ValidationEventBeforeSave);
        await SaveUpdate(gallery, CacheKeys.Gallery);

        return new GalleryActionResponse(gallery.Id, await validatorHelper.AfterEventAsync(gallery, Constants.ValidationEventAfterSave));
    }

    public async Task<GalleryActionResponse> DeleteAsync(int id)
    {
        var gallery = await GetGallery(id);

        await validatorHelper.ValidateAsync(gallery, Constants.ValidationEventBeforeDelete);
        await Delete(gallery, CacheKeys.Gallery);

        return new GalleryActionResponse(gallery.Id, await validatorHelper.AfterEventAsync(gallery, Constants.ValidationEventAfterDelete));
    }

    private async Task Delete(Gallery gallery, string cacheKey)
    {
        unitOfWork.Galleries.Delete(gallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task SaveAdd(Gallery gallery, string cacheKey)
    {
        await unitOfWork.Galleries.AddAsync(gallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task SaveUpdate(Gallery gallery, string cacheKey)
    {
        unitOfWork.Galleries.Update(gallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task CompleteContextAction(string cacheKey)
    {
        await unitOfWork.Complete();

        if (cacheKey != null)
            memoryCache.Remove(cacheKey);
    }

    private async Task<Gallery> GetGallery(long id)
    {
        var gallery = await unitOfWork.Galleries.ByIdAsync(id) ?? throw new GalleryNotFoundException("Gallery not found.");
        return gallery;
    }

    private static int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    }
}
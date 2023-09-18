using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Helpers.Interface;
using PhotographySite.Models;
using SwanSong.Service.Helpers.Exceptions;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Services;

public class GalleryService : IGalleryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IValidatorHelper<Gallery> _validatorHelper;

    public GalleryService(IUnitOfWork unitOfWork, 
                          IMapper mapper,
                          IValidatorHelper<Gallery> validatorHelper, 
                          IMemoryCache memoryCache)  
    {
        _unitOfWork = unitOfWork;
        _validatorHelper = validatorHelper;
        _memoryCache = memoryCache;
        _mapper = mapper; 
    }

    public async Task<GalleryResponse> GetGalleryAsync(long id)
    {
        return _mapper.Map<GalleryResponse>(await _unitOfWork.Galleries.ByIdAsync(id));
    }

    public async Task<List<GalleryResponse>> GetGalleriesAsync()
    {
        return _mapper.Map<List<GalleryResponse>>(await _unitOfWork.Galleries.AllSortedAsync());        
    } 

    public async Task<SearchPhotosResponse> SearchPhotosAsync(SearchPhotosRequest searchPhotosRequest)
    {
        var photoFilterRequest = PhotoFilterRequest.Create(searchPhotosRequest);

        var photos = _mapper.Map<List<PhotoResponse>>(await _unitOfWork.Photos.ByPagingAsync(photoFilterRequest));
        int numberOfPhotos = await _unitOfWork.Photos.ByFilterCountAsync(photoFilterRequest);
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
        var gallery = _mapper.Map<Gallery>(galleryAddRequest);

        await _validatorHelper.ValidateAsync(gallery, Constants.ValidationEventBeforeSave);
        await SaveAdd(gallery, CacheKeys.Gallery);

        return new GalleryActionResponse(gallery.Id, await _validatorHelper.AfterEventAsync(gallery, Constants.ValidationEventAfterSave));
    }

    public async Task<GalleryActionResponse> UpdateAsync(GalleryUpdateRequest galleryUpdateRequest)
    {
        var gallery = await GetGallery(galleryUpdateRequest.Id);

        gallery.Name = galleryUpdateRequest.Name;

        await _validatorHelper.ValidateAsync(gallery, Constants.ValidationEventBeforeSave);
        await SaveUpdate(gallery, CacheKeys.Gallery);

        return new GalleryActionResponse(gallery.Id, await _validatorHelper.AfterEventAsync(gallery, Constants.ValidationEventAfterSave));
    }

    public async Task<GalleryActionResponse> DeleteAsync(int id)
    {
        var gallery = await GetGallery(id);

        await _validatorHelper.ValidateAsync(gallery, Constants.ValidationEventBeforeDelete);
        await Delete(gallery, CacheKeys.Gallery);

        return new GalleryActionResponse(gallery.Id, await _validatorHelper.AfterEventAsync(gallery, Constants.ValidationEventAfterDelete));
    }

    private async Task Delete(Gallery gallery, string cacheKey)
    {
        _unitOfWork.Galleries.Delete(gallery);
        await CompleteContextAction(cacheKey);
    } 

    private async Task SaveAdd(Gallery gallery, string cacheKey)
    {
        await _unitOfWork.Galleries.AddAsync(gallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task SaveUpdate(Gallery gallery, string cacheKey)
    {
        _unitOfWork.Galleries.Update(gallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task CompleteContextAction(string cacheKey)
    {
        await _unitOfWork.Complete();

        if (cacheKey != null)
            _memoryCache.Remove(cacheKey);
    }

    private async Task<Gallery> GetGallery(long id)
    {
        var gallery = await _unitOfWork.Galleries.ByIdAsync(id);
        if (gallery == null)
            throw new GalleryNotFoundException("Gallery not found.");

        return gallery;
    } 

    private int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    }
}
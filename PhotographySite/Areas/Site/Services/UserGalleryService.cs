using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Helpers.Interface;
using PhotographySite.Models;
using SwanSong.Service.Helpers.Exceptions;
using System.Security.Authentication;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Services;

public class UserGalleryService : IUserGalleryService
{ 
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IValidatorHelper<UserGallery> _validatorHelper;

    public UserGalleryService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IValidatorHelper<UserGallery> validatorHelper,
                              IMemoryCache memoryCache)
    {
        _unitOfWork = unitOfWork;
        _validatorHelper = validatorHelper;
        _memoryCache = memoryCache;
        _mapper = mapper;
    }

    public async Task<UserGalleryResponse> GetUserGalleryAsync(Guid userId, long id)
    {
        UserGallery userGallery = await _unitOfWork.UserGalleries.GetFullGalleryAsync(userId, id);
        if (userGallery == null) 
            return new UserGalleryResponse(); 

		return _mapper.Map<UserGalleryResponse>(userGallery);  
    }

    public async Task<List<UserGalleryResponse>> GetUserGalleriesAsync(HttpContext httpContext)
    {
        Guid userId = GetUserId(httpContext.User.Identity.Name);

        if (userId != Guid.Empty)
            return _mapper.Map<List<UserGalleryResponse>>(await _unitOfWork.UserGalleries.AllSortedAsync(userId));

        return null;
    }

    public async Task<SearchPhotosResponse> SearchPhotosAsync(SearchPhotosRequest searchPhotosRequest)
    {
        PhotoFilterRequest photoFilterRequest = PhotoFilterRequest.Create(searchPhotosRequest);
         
        List<PhotoResponse> photos = _mapper.Map<List<PhotoResponse>>(await _unitOfWork.Photos.ByPagingAsync(photoFilterRequest));
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
     
    public async Task<UserGalleryActionResponse> AddAsync(UserGalleryAddRequest userGalleryAddRequest)
    {
        var userGallery = _mapper.Map<UserGallery>(userGalleryAddRequest);

        await _validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeSave);
        await SaveAdd(userGallery, CacheKeys.UserGallery);

        return new UserGalleryActionResponse(userGallery.Id, await _validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterSave));
    }

    public async Task<UserGalleryActionResponse> UpdateAsync(UserGalleryUpdateRequest galleryUpdateRequest)
    {
        var userGallery = await GetUserGallery(galleryUpdateRequest.Id);

        userGallery.Name = galleryUpdateRequest.Name;

        await _validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeSave);
        await SaveUpdate(userGallery, CacheKeys.Gallery);

        return new UserGalleryActionResponse(userGallery.Id, await _validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterSave));
    }

    public async Task<UserGalleryActionResponse> DeleteAsync(long id)
    {
        var userGallery = await GetUserGallery(id);

        await _validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeDelete);
        await Delete(userGallery, CacheKeys.Gallery);

        return new UserGalleryActionResponse(userGallery.Id, await _validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterDelete));
    }

    private async Task Delete(UserGallery userGallery, string cacheKey)
    {
        _unitOfWork.UserGalleries.Delete(userGallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task SaveAdd(UserGallery userGallery, string cacheKey)
    {
        await _unitOfWork.UserGalleries.AddAsync(userGallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task SaveUpdate(UserGallery userGallery, string cacheKey)
    {
        _unitOfWork.UserGalleries.Update(userGallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task CompleteContextAction(string cacheKey)
    {
        await _unitOfWork.Complete();

        if (cacheKey != null)
            _memoryCache.Remove(cacheKey);
    }

    private async Task<UserGallery> GetUserGallery(long id)
    {
        var userGallery = await _unitOfWork.UserGalleries.ByIdAsync(id);
        if (userGallery == null)
            throw new UserGalleryNotFoundException("User gallery not found.");

        return userGallery;
    }

    public Guid GetUserId(string username)
    {
        if (username != null)
        {
            Guid userId = _unitOfWork.Users.GetUserId(username);
            if (userId.Equals(Guid.Empty))
                throw new AuthenticationException();

            return userId;
        }

        return new Guid();
    } 

    private int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    } 
}
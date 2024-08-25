using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;
using PhotographySite.Helpers.Interface;
using PhotographySite.Models;
using SwanSong.Service.Helpers.Exceptions;
using System.Security.Authentication;

namespace PhotographySite.Areas.Site.Services;

public class UserGalleryService : IUserGalleryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _memoryCache;
    private readonly IValidatorHelper<UserGallery> _validatorHelper;
    private IPhotoCatalogService _photoCatalogService;
    private IUserGalleryPhotoService _userGalleryPhotoService;

    public UserGalleryService(IUnitOfWork unitOfWork,
                              IMapper mapper,
                              IValidatorHelper<UserGallery> validatorHelper,
                              IMemoryCache memoryCache,
                              IPhotoCatalogService photoCatalogService,
                              IUserGalleryPhotoService userGalleryPhotoService)
    {
        _unitOfWork = unitOfWork;
        _validatorHelper = validatorHelper;
        _memoryCache = memoryCache;
        _mapper = mapper;
        _photoCatalogService = photoCatalogService;
        _userGalleryPhotoService = userGalleryPhotoService;
    }

    public async Task<UserGalleryToEditResponse> GetUserGalleryToEditAsync(Guid userId, long id)
    {
        if (!await UserGalleryExists(userId, id))
            throw new UserGalleryNotFoundException("User Gallary not found.");

        return new UserGalleryToEditResponse()
        {
            SelectedGallery = await GetUserGalleryAsync(userId, id),
            SelectGalleryPhotos = await _userGalleryPhotoService.GetGalleryPhotosAsync(userId, id),
            LookupsResponse = await _photoCatalogService.GetLookupsAsync(),
            GalleryResponseList = await GetUserGalleriesAsync(userId)
        };
    }

    public async Task<UserGalleryResponse> GetUserGalleryAsync(Guid userId, long id)
    {
        if (!await UserGalleryExists(userId, id))
            throw new UserGalleryNotFoundException("User Gallary not found.");

        return _mapper.Map<UserGalleryResponse>(await _unitOfWork.UserGalleries.GetFullGalleryAsync(userId, id));
    }

    public async Task<List<UserGalleryResponse>> GetUserGalleriesAsync(HttpContext httpContext)
    {
        Guid userId = GetUserId(httpContext.User.Identity.Name);

        if (userId != Guid.Empty)
            return _mapper.Map<List<UserGalleryResponse>>(await _unitOfWork.UserGalleries.AllSortedAsync(userId));

        return null;
    }

    private async Task<List<UserGalleryResponse>> GetUserGalleriesAsync(Guid userId)
    {
        return _mapper.Map<List<UserGalleryResponse>>(await _unitOfWork.UserGalleries.AllSortedAsync(userId));
    }

    public async Task<SearchPhotosResponse> SearchPhotosAsync(SearchPhotosRequest request)
    {
        PhotoFilterRequest photoFilterRequest = PhotoFilterRequest.Create(request);

        List<PhotoResponse> photos = _mapper.Map<List<PhotoResponse>>(await _unitOfWork.Photos.ByPagingAsync(photoFilterRequest));
        int numberOfPhotos = await _unitOfWork.Photos.ByFilterCountAsync(photoFilterRequest);
        int numberOfPages = GetNumberOfPages(numberOfPhotos, photoFilterRequest.PageSize);

        return new SearchPhotosResponse()
        {
            NumberOfPhotos = numberOfPhotos,
            PageIndex = request.PageIndex + 1,
            PageSize = request.PageSize,
            NumberOfPages = numberOfPages,
            Photos = photos
        };
    }

    public async Task<UserGalleryActionResponse> AddAsync(UserGalleryAddRequest request)
    {
        var userGallery = _mapper.Map<UserGallery>(request);

        await _validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeSave);
        await SaveAdd(userGallery, CacheKeys.UserGallery);

        return new UserGalleryActionResponse(userGallery.Id, await _validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterSave));
    }

    public async Task<UserGalleryActionResponse> UpdateAsync(UserGalleryUpdateRequest request)
    {
        var userGallery = await GetUserGallery(request.UserId, request.Id);

        userGallery.Name = request.Name;

        await _validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeSave);
        await SaveUpdate(userGallery, CacheKeys.Gallery);

        return new UserGalleryActionResponse(userGallery.Id, await _validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterSave));
    }

    public async Task<UserGalleryActionResponse> DeleteAsync(Guid userId, long id)
    {
        var userGallery = await GetUserGallery(userId, id);

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

    private async Task<UserGallery> GetUserGallery(Guid userId, long id)
    {
        var userGallery = await _unitOfWork.UserGalleries.GetAsync(userId, id);
        if (userGallery == null)
            throw new UserGalleryNotFoundException("User gallery not found.");

        return userGallery;
    }

    private Guid GetUserId(string username)
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

    private async Task<bool> UserGalleryExists(Guid userId, long id)
    {
        return await _unitOfWork.UserGalleries.GetAsync(userId, id) != null;
    }
}
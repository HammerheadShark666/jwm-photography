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

public class UserGalleryService(IUnitOfWork unitOfWork,
                          IMapper mapper,
                          IValidatorHelper<UserGallery> validatorHelper,
                          IMemoryCache memoryCache,
                          IPhotoCatalogService photoCatalogService,
                          IUserGalleryPhotoService userGalleryPhotoService) : IUserGalleryService
{
    public async Task<UserGalleryToEditResponse> GetUserGalleryToEditAsync(Guid userId, long id)
    {
        if (!await UserGalleryExists(userId, id))
            throw new UserGalleryNotFoundException("User Gallary not found.");

        return new UserGalleryToEditResponse()
        {
            SelectedGallery = await GetUserGalleryAsync(userId, id),
            SelectGalleryPhotos = await userGalleryPhotoService.GetGalleryPhotosAsync(userId, id),
            LookupsResponse = await photoCatalogService.GetLookupsAsync(),
            GalleryResponseList = await GetUserGalleriesAsync(userId)
        };
    }

    public async Task<UserGalleryResponse> GetUserGalleryAsync(Guid userId, long id)
    {
        if (!await UserGalleryExists(userId, id))
            throw new UserGalleryNotFoundException("User Gallary not found.");

        return mapper.Map<UserGalleryResponse>(await unitOfWork.UserGalleries.GetFullGalleryAsync(userId, id));
    }

    public async Task<List<UserGalleryResponse>> GetUserGalleriesAsync(HttpContext httpContext)
    {
        Guid userId = GetUserId(httpContext.User.Identity.Name);

        if (userId != Guid.Empty)
            return mapper.Map<List<UserGalleryResponse>>(await unitOfWork.UserGalleries.AllSortedAsync(userId));

        return null;
    }

    private async Task<List<UserGalleryResponse>> GetUserGalleriesAsync(Guid userId)
    {
        return mapper.Map<List<UserGalleryResponse>>(await unitOfWork.UserGalleries.AllSortedAsync(userId));
    }

    public async Task<SearchPhotosResponse> SearchPhotosAsync(SearchPhotosRequest request)
    {
        PhotoFilterRequest photoFilterRequest = PhotoFilterRequest.Create(request);

        List<PhotoResponse> photos = mapper.Map<List<PhotoResponse>>(await unitOfWork.Photos.ByPagingAsync(photoFilterRequest));
        int numberOfPhotos = await unitOfWork.Photos.ByFilterCountAsync(photoFilterRequest);
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
        var userGallery = mapper.Map<UserGallery>(request);

        await validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeSave);
        await SaveAdd(userGallery, CacheKeys.UserGallery);

        return new UserGalleryActionResponse(userGallery.Id, await validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterSave));
    }

    public async Task<UserGalleryActionResponse> UpdateAsync(UserGalleryUpdateRequest request)
    {
        var userGallery = await GetUserGallery(request.UserId, request.Id);

        userGallery.Name = request.Name;

        await validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeSave);
        await SaveUpdate(userGallery, CacheKeys.Gallery);

        return new UserGalleryActionResponse(userGallery.Id, await validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterSave));
    }

    public async Task<UserGalleryActionResponse> DeleteAsync(Guid userId, long id)
    {
        var userGallery = await GetUserGallery(userId, id);

        await validatorHelper.ValidateAsync(userGallery, Constants.ValidationEventBeforeDelete);
        await Delete(userGallery, CacheKeys.Gallery);

        return new UserGalleryActionResponse(userGallery.Id, await validatorHelper.AfterEventAsync(userGallery, Constants.ValidationEventAfterDelete));
    }

    private async Task Delete(UserGallery userGallery, string cacheKey)
    {
        unitOfWork.UserGalleries.Delete(userGallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task SaveAdd(UserGallery userGallery, string cacheKey)
    {
        await unitOfWork.UserGalleries.AddAsync(userGallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task SaveUpdate(UserGallery userGallery, string cacheKey)
    {
        unitOfWork.UserGalleries.Update(userGallery);
        await CompleteContextAction(cacheKey);
    }

    private async Task CompleteContextAction(string cacheKey)
    {
        await unitOfWork.Complete();

        if (cacheKey != null)
            memoryCache.Remove(cacheKey);
    }

    private async Task<UserGallery> GetUserGallery(Guid userId, long id)
    {
        var userGallery = await unitOfWork.UserGalleries.GetAsync(userId, id) ?? throw new UserGalleryNotFoundException("User gallery not found.");
        return userGallery;
    }

    private Guid GetUserId(string username)
    {
        if (username != null)
        {
            Guid userId = unitOfWork.Users.GetUserId(username);
            if (userId.Equals(Guid.Empty))
                throw new AuthenticationException();

            return userId;
        }

        return new Guid();
    }

    private static int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    }

    private async Task<bool> UserGalleryExists(Guid userId, long id)
    {
        return await unitOfWork.UserGalleries.GetAsync(userId, id) != null;
    }
}
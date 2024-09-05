using AutoMapper;
using PhotographySite.Areas.Admin.Dto.Request;
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

namespace PhotographySite.Areas.Site.Services;

public class FavouriteService(IUnitOfWork unitOfWork, IMapper mapper, IValidatorHelper<Favourite> validatorHelper) : IFavouriteService
{
    public async Task<FavouritesResponse> AllAsync(Guid userId)
    {
        return new FavouritesResponse()
        {
            Favourites = mapper.Map<List<FavouriteResponse>>(await unitOfWork.Favourites.GetFavouritePhotosAsync(userId))
        };
    }

    public async Task<FavouriteActionResponse> AddAsync(FavouriteAddRequest favouriteAddRequest)
    {
        var favourite = mapper.Map<Favourite>(favouriteAddRequest);

        await validatorHelper.ValidateAsync(favourite, Constants.ValidationEventBeforeSave);
        await SaveAdd(favourite);

        return new FavouriteActionResponse(await validatorHelper.AfterEventAsync(favourite, Constants.ValidationEventAfterSave));
    }

    public async Task<FavouriteActionResponse> DeleteAsync(Guid userId, long photoId)
    {
        var favourite = await GetFavourite(userId, photoId);

        await validatorHelper.ValidateAsync(favourite, Constants.ValidationEventBeforeDelete);
        await Delete(favourite);

        return new FavouriteActionResponse(await validatorHelper.AfterEventAsync(favourite, Constants.ValidationEventAfterDelete));
    }

    private async Task Delete(Favourite favourite)
    {
        unitOfWork.Favourites.Delete(favourite);
        await CompleteContextAction();
    }

    private async Task SaveAdd(Favourite favourite)
    {
        await unitOfWork.Favourites.AddAsync(favourite);
        await CompleteContextAction();
    }

    private async Task CompleteContextAction()
    {
        await unitOfWork.Complete();
    }

    private async Task<Favourite> GetFavourite(Guid userId, long photoId)
    {
        var favourite = await unitOfWork.Favourites.GetFavouritePhotoAsync(userId, photoId) ?? throw new FavouriteNotFoundException("Favourite not found.");
        return favourite;
    }

    public async Task<SearchPhotosResponse> SearchPhotosAsync(Guid userId, SearchPhotosRequest searchPhotosRequest)
    {
        PhotoFilterRequest photoFilterRequest = PhotoFilterRequest.Create(searchPhotosRequest);

        var photos = mapper.Map<List<PhotoResponse>>(await unitOfWork.Favourites.GetFavouritePhotoByPagingAsync(userId, photoFilterRequest));
        int numberOfPhotos = await unitOfWork.Favourites.ByFilterCountAsync(userId, photoFilterRequest);
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

    private static int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    }
}
using AutoMapper;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Helpers.Interface;
using PhotographySite.Models;
using SwanSong.Service.Helpers.Exceptions;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Services;

public class FavouriteService : IFavouriteService
{	 
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private readonly IValidatorHelper<Favourite> _validatorHelper;

    public FavouriteService(IUnitOfWork unitOfWork, IMapper mapper, IValidatorHelper<Favourite> validatorHelper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validatorHelper = validatorHelper;
    }
     
    public async Task<FavouritesResponse> AllAsync(Guid userId)
    {
		return new FavouritesResponse()
        {
            Favourites = _mapper.Map<List<FavouriteResponse>>(await _unitOfWork.Favourites.GetFavouritePhotosAsync(userId)) 
        }; 
    } 

    public async Task<FavouriteActionResponse> AddAsync(FavouriteAddRequest favouriteAddRequest)
    {
        var favourite = _mapper.Map<Favourite>(favouriteAddRequest);

        await _validatorHelper.ValidateAsync(favourite, Constants.ValidationEventBeforeSave);
        await SaveAdd(favourite);

        return new FavouriteActionResponse(await _validatorHelper.AfterEventAsync(favourite, Constants.ValidationEventAfterSave));
    } 

    public async Task<FavouriteActionResponse> DeleteAsync(Guid userId, long photoId)
    {
        var favourite = await GetFavourite(userId, photoId);

        await _validatorHelper.ValidateAsync(favourite, Constants.ValidationEventBeforeDelete);
        await Delete(favourite);

        return new FavouriteActionResponse(await _validatorHelper.AfterEventAsync(favourite, Constants.ValidationEventAfterDelete));
    }

    private async Task Delete(Favourite favourite)
    {
        _unitOfWork.Favourites.Delete(favourite);
        await CompleteContextAction();
    }

    private async Task SaveAdd(Favourite favourite)
    {
        await _unitOfWork.Favourites.AddAsync(favourite);
        await CompleteContextAction();
    }
  
    private async Task CompleteContextAction()
    {
        await _unitOfWork.Complete(); 
    }

    private async Task<Favourite> GetFavourite(Guid userId, long photoId)
    {
        var favourite = await _unitOfWork.Favourites.GetFavouritePhotoAsync(userId, photoId);
        if (favourite == null)
            throw new FavouriteNotFoundException("Favourite not found.");

        return favourite;
    }
     
    public async Task<SearchPhotosResponse> SearchPhotosAsync(Guid userId, SearchPhotosRequest searchPhotosRequest)
    {
        PhotoFilterRequest photoFilterRequest = PhotoFilterRequest.Create(searchPhotosRequest); 

        var photos = _mapper.Map<List<PhotoResponse>>(await _unitOfWork.Favourites.GetFavouritePhotoByPagingAsync(userId, photoFilterRequest));
        int numberOfPhotos = await _unitOfWork.Favourites.ByFilterCountAsync(userId, photoFilterRequest);
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
 
    private int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    } 
}
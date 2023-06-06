using AutoMapper;
using FluentValidation;
using PhotographySite.Areas.Admin.Business;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Services;

public class FavouriteService : IFavouriteService
{
	private IUnitOfWork _unitOfWork;
	private IMapper _mapper;
	private IValidator<Favourite> _favouriteValidator;

	public FavouriteService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Favourite> favouriteValidator)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_favouriteValidator = favouriteValidator;
	} 

    public async Task<FavouritesDto> AllAsync(Guid userId)
    { 
        List<FavouriteDto> favourites = _mapper.Map<List<FavouriteDto>>(await _unitOfWork.Favourites.GetFavouritePhotosAsync(userId));
         
        FavouritesDto favouritesDto = new FavouritesDto()
        {
            Favourites = favourites,
            AzureStoragePath = EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl()
        };

        return favouritesDto;
    }

	public async Task AddAsync(Guid userId, long photoId)
	{
        Favourite favourite = new Favourite()
        {
            UserId = userId,
            PhotoId = photoId
        };

        await _unitOfWork.Favourites.AddAsync(favourite);

        _unitOfWork.Complete();
    }
     

    public async Task DeleteAsync(Guid userId, long photoId)
	{
		Favourite currentFavourite = await _unitOfWork.Favourites.GetFavouritePhotoAsync(userId, photoId);

		if (currentFavourite != null)
		{
			_unitOfWork.Favourites.Remove(currentFavourite);
			_unitOfWork.Complete();
		}
	} 
}

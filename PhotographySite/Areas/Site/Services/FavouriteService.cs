using AutoMapper;
using FluentValidation;
using PhotographySite.Areas.Admin.Dtos;
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

    public async Task<PhotosPageDto> GetPhotosPageAsync(PhotoFilterDto photoFilterDto)
    {
        return new PhotosPageDto()
        {
            Data = _mapper.Map<List<PhotoDto>>(await _unitOfWork.Photos.ByPagingAsync(photoFilterDto)),
            ItemsCount = await _unitOfWork.Photos.ByFilterCountAsync(photoFilterDto),
            AzureStoragePhotosContainerUrl = EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(),
        };
    }
     
    public async Task<SearchPhotosResultsDto> SearchPhotosAsync(Guid userId, SearchPhotosDto searchPhotosDto)
    {
        PhotoFilterDto photoFilterDto = new()
        {
            Title = searchPhotosDto.Title,
            CountryId = searchPhotosDto.CountryId,
            CategoryId = searchPhotosDto.CategoryId,
            PaletteId = searchPhotosDto.PaletteId,
            PageIndex = searchPhotosDto.PageIndex,
            PageSize = searchPhotosDto.PageSize
        };
         
        List<PhotoDto> photos = _mapper.Map<List<PhotoDto>>(await _unitOfWork.Favourites.GetFavouritePhotoByPagingAsync(userId, photoFilterDto));
        int numberOfPhotos = await _unitOfWork.Favourites.ByFilterCountAsync(userId, photoFilterDto);
        int numberOfPages = GetNumberOfPages(numberOfPhotos, photoFilterDto.PageSize);

        return new SearchPhotosResultsDto()
        {
            NumberOfPhotos = numberOfPhotos,
            PageIndex = searchPhotosDto.PageIndex + 1,
            PageSize = searchPhotosDto.PageSize,
            NumberOfPages = numberOfPages,
            Photos = photos,
            AzureStoragePhotosContainerUrl = EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl()
        };
    }

    private int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    }
}
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Services;

public class UserGalleryService : BaseService, IUserGalleryService
{
    private IUnitOfWork _unitOfWork;
	private IMapper _mapper;
	private IValidator<UserGallery> _userGalleryValidator;

	public UserGalleryService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserGallery> userGalleryValidator, IUserService userService) : base(userService)
    { 
		_unitOfWork = unitOfWork;
		_mapper = mapper;
        _userGalleryValidator = userGalleryValidator; 
    }

    public async Task<UserGalleryDto> GetUserGalleryAsync(Guid userId, long id)
    {
        UserGallery userGallery = await _unitOfWork.UserGalleries.GetFullGalleryAsync(userId, id);

        if (userGallery == null) 
            return new UserGalleryDto(); 

		UserGalleryDto userGalleryDto = _mapper.Map<UserGalleryDto>(userGallery);
		userGalleryDto.AzureStoragePath = EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl();
		return userGalleryDto; 
    }

    public async Task<List<UserGalleryDto>> GetUserGalleriesAsync(HttpContext httpContext)
    { 
		Guid userId = GetUserId(httpContext.User.Identity.Name);

        if (userId != Guid.Empty) 
            return _mapper.Map<List<UserGalleryDto>>(await _unitOfWork.UserGalleries.AllSortedAsync(userId)); 

        return null;
    }

    public async Task<SearchPhotosResultsDto> SearchPhotosAsync(SearchPhotosDto searchPhotosDto)
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

        List<PhotoDto> photos = _mapper.Map<List<PhotoDto>>(await _unitOfWork.Photos.ByPagingAsync(photoFilterDto));
        int numberOfPhotos = await _unitOfWork.Photos.ByFilterCountAsync(photoFilterDto);
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

    public async Task<UserGalleryNameDto> SaveName(UserGalleryNameDto userGalleryNameDto)
    {
        UserGallery userGallery = await _unitOfWork.UserGalleries.GetAsync(userGalleryNameDto.UserId, userGalleryNameDto.Id);
         
        if(userGallery == null)
            throw new UnauthorizedAccessException("You are not authorised to make changes to this gallery.");

        userGallery.Name = userGalleryNameDto.Name;

        ValidationResult validationResult = await _userGalleryValidator.ValidateAsync(userGallery, options => options
                                                                                .IncludeRuleSets("BeforeSave"));

        if (validationResult.IsValid)
        {             
            _unitOfWork.UserGalleries.Update(userGallery);
            _unitOfWork.Complete();

            userGalleryNameDto.IsValid = true;
        }
        else
            userGalleryNameDto.Errors = validationResult.Errors;        

        return userGalleryNameDto;
    }

    public async Task<UserGalleryNameDto> SaveNewUserGalleryAsync(UserGalleryNameDto userGalleryNameDto)
    {
        UserGallery userGallery = _mapper.Map<UserGallery>(userGalleryNameDto);

        ValidationResult validationResult = await _userGalleryValidator.ValidateAsync(userGallery, options => options
                                                                                .IncludeRuleSets("BeforeSave"));

        if (validationResult.IsValid)
        {
            userGallery = await _unitOfWork.UserGalleries.AddAsync(userGallery);
            _unitOfWork.Complete();

            userGalleryNameDto = _mapper.Map<UserGalleryNameDto>(userGallery);
            userGalleryNameDto.IsValid = true; 
        } 
        else
        {
            userGalleryNameDto.Errors = validationResult.Errors;
            userGalleryNameDto.IsValid= validationResult.IsValid;
        }

        return userGalleryNameDto;  
    }

    public async Task DeleteAsync(long id)
    {
        UserGallery currentGallery = await _unitOfWork.UserGalleries.ByIdAsync(id);

        if (currentGallery != null)
        {
            _unitOfWork.UserGalleries.Remove(currentGallery);
            _unitOfWork.Complete();
        }
    }
}

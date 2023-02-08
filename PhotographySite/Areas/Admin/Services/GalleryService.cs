using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services;

public class GalleryService : IGalleryService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IValidator<Gallery> _galleryValidator;

    public GalleryService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Gallery> galleryValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _galleryValidator = galleryValidator;
    }

    public async Task<GalleryDto> GetGalleryAsync(long id)
    {
        return _mapper.Map<GalleryDto>(await _unitOfWork.Galleries.ByIdAsync(id));
    }

    public async Task<List<GalleryDto>> GetGalleriesAsync()
    {
        return _mapper.Map<List<GalleryDto>>(await _unitOfWork.Galleries.AllSortedAsync());        
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
            Photos = photos 
        };  
    }

    private int GetNumberOfPages(int numberOfPhotos, int pageSize)
    {
        //int numberOfPhotos = await _unitOfWork.Photos.ByFilterCountAsync(photoFilterDto);
       // return (numberOfPhotos / photoFilterDto.PageSize) + ((numberOfPhotos % photoFilterDto.PageSize > 0) ? 1 : 0);

        return (numberOfPhotos / pageSize) + ((numberOfPhotos % pageSize > 0) ? 1 : 0);
    }     

    public async Task<GalleryNameDto> SaveName(GalleryNameDto galleryNameDto)
    {
        Gallery gallery = await _unitOfWork.Galleries.ByIdAsync(galleryNameDto.Id);
        gallery.Name = galleryNameDto.Name;

        ValidationResult validationResult = await _galleryValidator.ValidateAsync(gallery, options => options
                                                                                .IncludeRuleSets("BeforeSave"));

        if (validationResult.IsValid)
        {             
            _unitOfWork.Galleries.Update(gallery);
            _unitOfWork.Complete();

            galleryNameDto.IsValid = true;
        }
        else
        {
            galleryNameDto.Errors = validationResult.Errors;            
        }

        return galleryNameDto;
    }

    public async Task<GalleryNameDto> SaveNewGalleryAsync(GalleryNameDto galleryNameDto)
    {
        Gallery gallery = _mapper.Map<Gallery>(galleryNameDto);

        ValidationResult validationResult = await _galleryValidator.ValidateAsync(gallery, options => options
                                                                                .IncludeRuleSets("BeforeSave"));

        if (validationResult.IsValid)
        {
            gallery = await _unitOfWork.Galleries.AddAsync(gallery);
            _unitOfWork.Complete();

            galleryNameDto = _mapper.Map<GalleryNameDto>(gallery);
            galleryNameDto.IsValid = true; 
        } 
        else
        {
            galleryNameDto.Errors = validationResult.Errors;
            galleryNameDto.IsValid= validationResult.IsValid;
        }

        return galleryNameDto;  
    }

    public async Task DeleteAsync(long id)
    {
        Gallery currentGallery = await _unitOfWork.Galleries.ByIdAsync(id);

        if (currentGallery != null)
        {
            _unitOfWork.Galleries.Remove(currentGallery);
            _unitOfWork.Complete();
        }
    }
}

using AutoMapper;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services;

public class PhotoCatalogService : IPhotoCatalogService
{    
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public PhotoCatalogService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<LookupsDto> GetLookupsAsync()
    {
       return new LookupsDto()
       {
            Categories = _mapper.Map<List<CategoryDto>>(await _unitOfWork.Categories.AllSortedAsync()),
            Countries = _mapper.Map<List<CountryDto>>(await _unitOfWork.Countries.AllSortedAsync()),
            Palettes = _mapper.Map<List<PaletteDto>>(await _unitOfWork.Palettes.AllSortedAsync())
       };
    }

    public async Task<PhotosPageDto> GetPhotosPageAsync(PhotoFilterDto photoFilterDto)
    {         
        return new PhotosPageDto() {
            Data = _mapper.Map<List<PhotoDto>>(await _unitOfWork.Photos.ByPagingAsync(photoFilterDto)),
            ItemsCount = await _unitOfWork.Photos.ByFilterCountAsync(photoFilterDto)
        };       
    }
  
    public async Task UpdatePhotoAsync(UpdatePhotoDto updatePhotoDto) {

        Photo  photo = await _unitOfWork.Photos.ByIdAsync(updatePhotoDto.Id);

        if(photo != null)
        {
            photo.Title = updatePhotoDto.Title;
            photo.Country =  await _unitOfWork.Countries.ByIdAsync(updatePhotoDto.CountryId);
            photo.Category = await _unitOfWork.Categories.ByIdAsync(updatePhotoDto.CategoryId);
            photo.Palette = await _unitOfWork.Palettes.ByIdAsync(updatePhotoDto.PaletteId);

            _unitOfWork.Photos.Update(photo);
            _unitOfWork.Complete();            
        }
    }
}
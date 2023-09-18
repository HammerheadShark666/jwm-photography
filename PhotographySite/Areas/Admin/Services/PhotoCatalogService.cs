using AutoMapper;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;

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

    public async Task<LookupsResponse> GetLookupsAsync()
    {
       return new LookupsResponse()
       {
            Categories = _mapper.Map<List<CategoryResponse>>(await _unitOfWork.Categories.AllSortedAsync()),
            Countries = _mapper.Map<List<CountryResponse>>(await _unitOfWork.Countries.AllSortedAsync()),
            Palettes = _mapper.Map<List<PaletteResponse>>(await _unitOfWork.Palettes.AllSortedAsync())
       };
    }

    public async Task<PhotoPageResponse> GetPhotosPageAsync(PhotoFilterRequest photoFilterRequest)
    {         
        return new PhotoPageResponse() {
            Data = _mapper.Map<List<PhotoResponse>>(await _unitOfWork.Photos.ByPagingAsync(photoFilterRequest)),
            ItemsCount = await _unitOfWork.Photos.ByFilterCountAsync(photoFilterRequest)  
        };       
    }
  
    public async Task UpdatePhotoAsync(UpdatePhotoRequest updatePhotoRequest) {

        var photo = await _unitOfWork.Photos.ByIdAsync(updatePhotoRequest.Id);

        if(photo != null)
        {
            photo.Title = updatePhotoRequest.Title;
            photo.Country =  await _unitOfWork.Countries.ByIdAsync(updatePhotoRequest.CountryId);
            photo.Category = await _unitOfWork.Categories.ByIdAsync(updatePhotoRequest.CategoryId);
            photo.Palette = await _unitOfWork.Palettes.ByIdAsync(updatePhotoRequest.PaletteId);

            _unitOfWork.Photos.Update(photo);
            await _unitOfWork.Complete();            
        }
    } 

    public async Task<List<PhotoResponse>> GetLatestPhotos(int numberOfPhotos)
    {
        return _mapper.Map<List<PhotoResponse>>(await _unitOfWork.Photos.GetLatestPhotos(numberOfPhotos));
    } 
}
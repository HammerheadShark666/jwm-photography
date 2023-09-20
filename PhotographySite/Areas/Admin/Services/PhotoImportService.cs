using AutoMapper;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Helpers.Interface;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services;

public class PhotoImportService : IPhotoImportService
{ 
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IAzureStorageBlobHelper _azureStorageBlobHelper;

    public PhotoImportService(IUnitOfWork unitOfWork, IMapper mapper, IAzureStorageBlobHelper azureStorageBlobHelper)
    {
        _unitOfWork = unitOfWork; 
        _mapper = mapper;
        _azureStorageBlobHelper = azureStorageBlobHelper;
    }

    public async Task<SavedPhotosResponse> ImportAsync(List<IFormFile> photos)
    {
        string directoryPath = EnvironmentVariablesHelper.TempPhotoDirectoryPath;
         
        var (existingPhotos, newPhotos) = await GetExistingNewPhotoListAsync(photos); 

        await _azureStorageBlobHelper.SaveBlobsToAzureStorageContainerAsync(newPhotos, Constants.AzureStorageContainerName);

        var fileNames = await FileHelper.SaveFilesToDirectoryAsync(newPhotos, directoryPath);
        var photosWithDetails = GetPhotoDetails(fileNames, directoryPath);

        var savedPhotosResponse = await GetLookUpsAsync(new SavedPhotosResponse()); 
        savedPhotosResponse.SavedPhotos = SavePhotos(photosWithDetails);
        savedPhotosResponse.ExistingPhotos = existingPhotos; 

        FileHelper.DeleteAllFilesInDirectory(directoryPath);

        return savedPhotosResponse;
    }

    public async Task<SavedPhotosResponse> GetLookUpsAsync(SavedPhotosResponse savedPhotosResponse)
    {
        savedPhotosResponse.Lookups.Categories = _mapper.Map<List<CategoryResponse>>(await _unitOfWork.Categories.AllSortedAsync());
        savedPhotosResponse.Lookups.Countries = _mapper.Map<List<CountryResponse>>(await _unitOfWork.Countries.AllSortedAsync());
        savedPhotosResponse.Lookups.Palettes = _mapper.Map<List<PaletteResponse>>(await _unitOfWork.Palettes.AllSortedAsync());
       
        return savedPhotosResponse;
    }

    private async Task<(List<ExistingPhotoRequest>, List<IFormFile>)> GetExistingNewPhotoListAsync(List<IFormFile> photos)
    {
        var existingPhotos = new List<ExistingPhotoRequest>();
        var newPhotos = new List<IFormFile>();

        foreach (IFormFile photo in photos)
        {
            var existingPhoto = await _unitOfWork.Photos.FindByFilenameAsync(photo.FileName);
            if (existingPhoto != null)
            {
                existingPhotos.Add(new ExistingPhotoRequest()
                {
                    FileName = existingPhoto.FileName,
                    Title = existingPhoto.Title,
                    DateTaken = existingPhoto.DateTaken
                });
            }
            else 
                newPhotos.Add(photo); 
        }

        return (existingPhotos, newPhotos);
    }
  
    private List<Photo> GetPhotoDetails(List<string> fileNames, string directoryPath)
    {
        var photos = new List<Photo>();

        foreach (var filename in fileNames)
        {
            Photo photo = ExifHelper.GetExifData(directoryPath + filename);
            photo.Orientation = (int)PhotoHelper.GetOrientation(photo.Width, photo.Height);
            photo.UseInMontage = true;
            photos.Add(photo);
        }

        return photos;
    }

    private List<Photo> SavePhotos(List<Photo> photos)
    {
        var savedPhotos = new List<Photo>();

        foreach (var photo in photos)
        {
            if (photo.Id == 0)
            {
                _unitOfWork.Photos.AddAsync(photo);
                savedPhotos.Add(photo);
            }
               
        }
            
        _unitOfWork.Complete();

        return savedPhotos;
    }
}
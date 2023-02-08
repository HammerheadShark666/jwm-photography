using AutoMapper;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services;

public class PhotoImportService : IPhotoImportService
{
    private readonly IConfiguration _configuration;
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public PhotoImportService(IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _mapper = mapper;
    }

    public async Task<SavedPhotosDto> ImportAsync(List<IFormFile> photos)
    {
        string directoryPath = _configuration.GetValue<string>("Photos:Directory");
         
        var (existingPhotos, newPhotos) = await GetExistingNewPhotoListAsync(photos);

        List<string> fileNames = await FileHelper.SaveFilesToDirectoryAsync(newPhotos, directoryPath);
        List<Photo> photosWithDetails = GetPhotoDetails(fileNames, directoryPath);
        List<Photo> savedPhotos = SavePhotos(photosWithDetails);

        SavedPhotosDto savedPhotosDto = new SavedPhotosDto()
        {
            SavedPhotos = savedPhotos,
            ExistingPhotos = existingPhotos,
            Categories = _mapper.Map<List<CategoryDto>>(await _unitOfWork.Categories.AllSortedAsync()),
            Countries = _mapper.Map<List<CountryDto>>(await _unitOfWork.Countries.AllSortedAsync()),
            Palettes = _mapper.Map<List<PaletteDto>>(await _unitOfWork.Palettes.AllSortedAsync()),
        };

        return savedPhotosDto;
    }

    private async Task<(List<ExistingPhotoDto>, List<IFormFile>)> GetExistingNewPhotoListAsync(List<IFormFile> photos)
    {
        List<ExistingPhotoDto> existingPhotos = new List<ExistingPhotoDto>();
        List<IFormFile> newPhotos = new List<IFormFile>();

        foreach (IFormFile photo in photos)
        {
            Photo existingPhoto = await _unitOfWork.Photos.FindByFilenameAsync(photo.FileName);

            if (existingPhoto != null)
            {
                existingPhotos.Add(new ExistingPhotoDto()
                {
                    FileName = existingPhoto.FileName,
                    Title = existingPhoto.Title,
                    DateTaken = existingPhoto.DateTaken
                });
            }
            else
            {
                newPhotos.Add(photo);
            }
        }

        return (existingPhotos, newPhotos);
    }

    private Photo Save(Photo photo)
    {
        if (photo.Id == 0)
            _unitOfWork.Photos.AddAsync(photo);

        _unitOfWork.Complete();

        return photo;
    }

    private List<Photo> GetPhotoDetails(List<string> fileNames, string directoryPath)
    {
        List<Photo> photos = new List<Photo>();

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
        List<Photo> savedPhotos = new List<Photo>();

        foreach (var photo in photos)
        {
            Photo savedPhoto = Save(photo);
            savedPhotos.Add(savedPhoto);
        }

        return savedPhotos;
    }
}
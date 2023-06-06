using AutoMapper;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Services;

public class MontageService : IMontageService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public MontageService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MontagesDto> GetMontageAsync(string username)
    {            
        return await GetMontagePhotos(username);
    } 

    public async Task<MontagesDto> GetMontagePhotos(string username)
    {
        Guid userId = _unitOfWork.Users.GetUserIdAsync(username);
        List<Montage> montages = await _unitOfWork.Montages.AllSortedAsync();
        List<Photo> squarePhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.square, userId);
        List<Photo> landscapePhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.landscape, userId);
        List<Photo> portraitPhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.portrait, userId);

        List<List<MontageDto>> montageImagesColumns = new List<List<MontageDto>>();
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 1, squarePhotos, portraitPhotos, landscapePhotos));
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 2, squarePhotos, portraitPhotos, landscapePhotos));
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 3, squarePhotos, portraitPhotos, landscapePhotos));
        montageImagesColumns.Add(GetMontageTemplatesForColumn(montages, 4, squarePhotos, portraitPhotos, landscapePhotos));

        return new MontagesDto()
        {
            MontageImagesColumns = montageImagesColumns,
            AzureStoragePath = EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl()
        };
    }

    private List<MontageDto> GetMontageTemplatesForColumn(List<Montage> montages, int column, List<Photo> squarePhotos, List<Photo> portraitPhotos, List<Photo> landscapePhotos)
    {
        return MontageHelper.AddMontagePhotos(_mapper.Map<List<MontageDto>>(GetColumnMontages(montages, column)), squarePhotos, portraitPhotos, landscapePhotos);
    }

    private List<Montage> GetColumnMontages(List<Montage> montages, int column)
    {
        return montages.FindAll(i => i.Column == column).OrderBy(i => i.Order).ToList();
    }
     
    private List<Photo> GetPhotos(List<Montage> montages, Helpers.Enums.PhotoOrientation orientation, Guid userId)
    {
        int numberOfPhotos = montages.Where(m => m.Orientation == orientation).Count();
        List<Photo> photos = _unitOfWork.Photos.MontagePhotos(orientation, numberOfPhotos, userId);

        return photos;
    }
}

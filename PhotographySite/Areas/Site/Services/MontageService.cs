using AutoMapper;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;
using PhotographySite.Models;

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

    public async Task<MontagesResponse> GetMontageAsync(string username)
    {            
        return await GetMontagePhotos(username);
    } 

    public async Task<MontagesResponse> GetMontagePhotos(string username)
    {
        Guid userId = _unitOfWork.Users.GetUserId(username);
        var montages = await _unitOfWork.Montages.AllSortedAsync();
        var squarePhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.square, userId);
        var landscapePhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.landscape, userId);
        var portraitPhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.portrait, userId);
          
        return new MontagesResponse()
        {
            MontageImagesColumns = new List<List<MontageResponse>>()
            {
                GetMontageTemplatesForColumn(montages, 1, squarePhotos, portraitPhotos, landscapePhotos),
                GetMontageTemplatesForColumn(montages, 2, squarePhotos, portraitPhotos, landscapePhotos),
                GetMontageTemplatesForColumn(montages, 3, squarePhotos, portraitPhotos, landscapePhotos),
                GetMontageTemplatesForColumn(montages, 4, squarePhotos, portraitPhotos, landscapePhotos)
            }
        };
    }

    private List<MontageResponse> GetMontageTemplatesForColumn(List<Montage> montages, int column, List<Photo> squarePhotos, List<Photo> portraitPhotos, List<Photo> landscapePhotos)
    {
        return MontageHelper.AddMontagePhotos(_mapper.Map<List<MontageResponse>>(GetColumnMontages(montages, column)), squarePhotos, portraitPhotos, landscapePhotos);
    }

    private List<Montage> GetColumnMontages(List<Montage> montages, int column)
    {
        return montages.FindAll(i => i.Column == column).OrderBy(i => i.Order).ToList();
    }
     
    private List<Photo> GetPhotos(List<Montage> montages, Helpers.Enums.PhotoOrientation orientation, Guid userId)
    {
        int numberOfPhotos = montages.Where(m => m.Orientation == orientation).Count();
        return _unitOfWork.Photos.MontagePhotos(orientation, numberOfPhotos, userId); 
    }
}
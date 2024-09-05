using AutoMapper;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;
using PhotographySite.Models;

namespace PhotographySite.Areas.Site.Services;

public class MontageService(IUnitOfWork unitOfWork, IMapper mapper) : IMontageService
{
    public async Task<MontagesResponse> GetMontageAsync(Guid userId)
    {
        return await GetMontagePhotos(userId);
    }

    public async Task<MontagesResponse> GetMontagePhotos(Guid userId)
    {
        //Guid userId = _unitOfWork.Users.GetUserId(username);
        var montages = await unitOfWork.Montages.AllSortedAsync();
        var squarePhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.square, userId);
        var landscapePhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.landscape, userId);
        var portraitPhotos = GetPhotos(montages, Helpers.Enums.PhotoOrientation.portrait, userId);

        return new MontagesResponse()
        {
            MontageImagesColumns =
            [
                GetMontageTemplatesForColumn(montages, 1, squarePhotos, portraitPhotos, landscapePhotos),
                GetMontageTemplatesForColumn(montages, 2, squarePhotos, portraitPhotos, landscapePhotos),
                GetMontageTemplatesForColumn(montages, 3, squarePhotos, portraitPhotos, landscapePhotos),
                GetMontageTemplatesForColumn(montages, 4, squarePhotos, portraitPhotos, landscapePhotos)
            ]
        };
    }

    private List<MontageResponse> GetMontageTemplatesForColumn(List<Montage> montages, int column, List<Photo> squarePhotos, List<Photo> portraitPhotos, List<Photo> landscapePhotos)
    {
        return MontageHelper.AddMontagePhotos(mapper.Map<List<MontageResponse>>(GetColumnMontages(montages, column)), squarePhotos, portraitPhotos, landscapePhotos);
    }

    private static List<Montage> GetColumnMontages(List<Montage> montages, int column)
    {
        return [.. montages.FindAll(i => i.Column == column).OrderBy(i => i.Order)];
    }

    private List<Photo> GetPhotos(List<Montage> montages, Helpers.Enums.PhotoOrientation orientation, Guid userId)
    {
        int numberOfPhotos = montages.Where(m => m.Orientation == orientation).Count();
        return unitOfWork.Photos.MontagePhotos(orientation, numberOfPhotos, userId);
    }
}
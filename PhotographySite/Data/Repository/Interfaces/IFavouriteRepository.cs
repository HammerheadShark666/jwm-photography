using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IFavouriteRepository
{
	Task<List<Photo>> GetFavouritePhotosAsync(Guid userId);

    Task<Favourite> GetFavouritePhotoAsync(Guid userId, long photoId);

    Task<int> ByFilterCountAsync(Guid userId, PhotoFilterRequest photoFilterRequest);

    Task<List<Photo>> GetFavouritePhotoByPagingAsync(Guid userId, PhotoFilterRequest photoFilterRequest);

    Task<Boolean> PhotoIsAlreadyAFavourite(Favourite favourite);

    void Delete(Favourite favourite);

    Task AddAsync(Favourite favourite);
}
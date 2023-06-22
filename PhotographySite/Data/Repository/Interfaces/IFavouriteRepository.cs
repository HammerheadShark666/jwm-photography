using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IFavouriteRepository : IBaseRepository<Favourite>
{
	Task<List<Photo>> GetFavouritePhotosAsync(Guid userId);

    Task<Favourite> GetFavouritePhotoAsync(Guid userId, long photoId);

    Task<int> ByFilterCountAsync(Guid userId, PhotoFilterDto photoFilterDto);

    Task<List<Photo>> GetFavouritePhotoByPagingAsync(Guid userId, PhotoFilterDto photoFilterDto);
}

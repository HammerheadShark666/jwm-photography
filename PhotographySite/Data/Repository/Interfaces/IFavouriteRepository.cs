using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IFavouriteRepository : IBaseRepository<Favourite>
{
	Task<List<Photo>> GetFavouritePhotosAsync(Guid userId);

    Task<Favourite> GetFavouritePhotoAsync(Guid userId, long photoId);
}

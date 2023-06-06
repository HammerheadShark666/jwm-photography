using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IFavouriteService
{
    Task<FavouritesDto> AllAsync(Guid userId);

    Task AddAsync(Guid userId, long photoId);

    Task DeleteAsync(Guid userId, long photoId);
}

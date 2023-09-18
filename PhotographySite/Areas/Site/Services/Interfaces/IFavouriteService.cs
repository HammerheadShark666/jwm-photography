using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IFavouriteService
{
    Task<FavouritesResponse> AllAsync(Guid userId);
    Task<FavouriteActionResponse> AddAsync(FavouriteAddRequest favouriteAddRequest);
    Task<FavouriteActionResponse> DeleteAsync(Guid userId, long photoId);
    Task<SearchPhotosResponse> SearchPhotosAsync(Guid userId, SearchPhotosRequest searchPhotosRequest);
}
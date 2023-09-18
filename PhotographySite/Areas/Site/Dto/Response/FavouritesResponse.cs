using PhotographySite.Helpers;

namespace PhotographySite.Areas.Site.Dto.Response;

public class FavouritesResponse
{
    public List<FavouriteResponse> Favourites { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(); }
}
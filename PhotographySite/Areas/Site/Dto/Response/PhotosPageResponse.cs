using PhotographySite.Dto.Response;
using PhotographySite.Helpers;

namespace PhotographySite.Areas.Site.Dto.Response;

public class PhotosPageResponse
{
    public int ItemsCount { get; set; }
    public List<PhotoResponse> Photos { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(); }
}
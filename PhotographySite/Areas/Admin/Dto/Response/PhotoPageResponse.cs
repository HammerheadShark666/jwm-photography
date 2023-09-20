using PhotographySite.Dto.Response;
using PhotographySite.Helpers;

namespace PhotographySite.Areas.Admin.Dto.Response;

public class PhotoPageResponse
{
    public int ItemsCount { get; set; }
    public List<PhotoResponse> Data { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl; }
}
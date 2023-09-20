using PhotographySite.Helpers;

namespace PhotographySite.Dto.Response;

public class SearchPhotosResponse
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int NumberOfPages { get; set; }
    public List<PhotoResponse> Photos { get; set; }
    public int NumberOfPhotos { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl; }
}
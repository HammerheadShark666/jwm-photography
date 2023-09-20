using PhotographySite.Helpers;

namespace PhotographySite.Dto.Response;

public class GalleryResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string RandomPhoto { get; set; }
    public List<GalleryPhotoResponse> Photos { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl; }
}
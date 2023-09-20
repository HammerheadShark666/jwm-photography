using PhotographySite.Helpers;

namespace PhotographySite.Areas.Site.Dto.Response;

public class UserGalleryResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string RandomPhoto { get; set; }
    public List<UserGalleryPhotoResponse> Photos { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl; }
}
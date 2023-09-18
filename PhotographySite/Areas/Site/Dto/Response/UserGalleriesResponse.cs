using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Dto.Response;
using PhotographySite.Helpers;

namespace PhotographySite.Areas.Site.Dto.Response;

public class UserGalleriesResponse
{
    public LookupsResponse LookupsResponse { get; set; }
    public List<UserGalleryResponse> GalleryResponseList { get; set; }
    public PhotoPageResponse Photos { get; set; }
    public UserGalleryResponse SelectedGallery { get; set; }
    public List<PhotoResponse> SelectGalleryPhotos { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(); }
}
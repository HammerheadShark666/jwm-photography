using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Helpers;

namespace PhotographySite.Dto.Response;

public class GalleriesResponse
{
    public LookupsResponse LookupsResponse { get; set; }
    public List<GalleryResponse> Galleries { get; set; }
    public PhotoPageResponse Photos { get; set; }
    public GalleryResponse SelectedGallery { get; set; }
    public List<PhotoResponse> SelectGalleryPhotos { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(); }
}
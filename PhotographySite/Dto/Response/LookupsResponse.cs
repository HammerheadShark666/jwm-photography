using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Helpers;

namespace PhotographySite.Dto.Response;

public class LookupsResponse
{
    public List<CountryResponse> Countries { get; set; }
    public List<CategoryResponse> Categories { get; set; }
    public List<PaletteResponse> Palettes { get; set; }
    public List<CameraResponse> Cameras { get; set; }
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(); }
}
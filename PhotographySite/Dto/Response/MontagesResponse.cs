using PhotographySite.Helpers;

namespace PhotographySite.Dto.Response;

public class MontagesResponse
{
    public List<List<MontageResponse>> MontageImagesColumns { get; set; } = new List<List<MontageResponse>>();
    public string AzureStoragePhotosContainerUrl { get => EnvironmentVariablesHelper.AzureStoragePhotosContainerUrl(); }
}
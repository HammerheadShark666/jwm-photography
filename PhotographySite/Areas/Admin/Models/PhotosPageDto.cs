namespace PhotographySite.Areas.Admin.Models;

public class PhotosPageDto
{
    public int ItemsCount { get; set; }
    public List<PhotoDto> Data { get; set; }
    public string AzureStoragePhotosContainerUrl { get; set; }
}

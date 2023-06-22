using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Dtos;

public class SavedPhotosDto : LookupsDto
{
    public List<Photo> SavedPhotos { get; set; }

    public List<ExistingPhotoDto> ExistingPhotos { get; set; }

    public string AzureStoragePhotosContainerUrl { get; set; }
}

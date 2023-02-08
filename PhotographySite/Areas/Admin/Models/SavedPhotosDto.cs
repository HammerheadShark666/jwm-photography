using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Models;

public class SavedPhotosDto : LookupsDto
{
    public List<Photo> SavedPhotos { get; set; }

    public List<ExistingPhotoDto> ExistingPhotos { get; set; }
}

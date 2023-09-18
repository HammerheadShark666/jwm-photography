using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Dto.Response;

public class SavedPhotosResponse
{
    public List<Photo> SavedPhotos { get; set; }
    public List<ExistingPhotoRequest> ExistingPhotos { get; set; }
    public LookupsResponse Lookups { get; set; }

    public SavedPhotosResponse()
    {
        Lookups = new LookupsResponse();
    }
}
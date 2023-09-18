using PhotographySite.Areas.Admin.Dto.Response;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IPhotoImportService
{
    Task<SavedPhotosResponse> ImportAsync(List<IFormFile> photos);
}
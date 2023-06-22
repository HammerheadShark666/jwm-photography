using PhotographySite.Areas.Admin.Dtos;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IPhotoImportService
{
    Task<SavedPhotosDto> ImportAsync(List<IFormFile> photos);
}

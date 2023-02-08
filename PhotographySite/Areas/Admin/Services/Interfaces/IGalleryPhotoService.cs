using PhotographySite.Areas.Admin.Models;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IGalleryPhotoService
{      
    Task<List<PhotoDto>> GetGalleryPhotosAsync(long id);

    Task<GalleryPhoto> AddPhotoToGalleryAsync(GalleryPhotoDto galleryPhotoDto);

    Task<GalleryPhoto> MovePhotoInGalleryAsync(GalleryPhotoDto galleryPhotoDto);

    Task RemovePhotoFromGalleryAsync(GalleryPhotoDto galleryPhotoDto);
}

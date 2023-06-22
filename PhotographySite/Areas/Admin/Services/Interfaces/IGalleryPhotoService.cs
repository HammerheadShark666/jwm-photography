using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IGalleryPhotoService
{      
    Task<List<PhotoDto>> GetGalleryPhotosAsync(long id);

    Task<GalleryPhoto> AddPhotoToGalleryAsync(GalleryPhotoDto galleryPhotoDto);

    Task<GalleryPhoto> MovePhotoInGalleryAsync(GalleryPhotoDto galleryPhotoDto);

    Task RemovePhotoFromGalleryAsync(GalleryPhotoDto galleryPhotoDto);
}

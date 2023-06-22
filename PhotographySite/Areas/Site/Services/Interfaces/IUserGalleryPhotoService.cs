using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IUserGalleryPhotoService
{      
    Task<List<PhotoDto>> GetGalleryPhotosAsync(long id);

    Task<UserGalleryPhoto> AddPhotoToUserGalleryAsync(UserGalleryPhotoDto galleryPhotoDto);

    Task<UserGalleryPhoto> MovePhotoInGalleryAsync(UserGalleryPhotoDto galleryPhotoDto);

    Task RemovePhotoFromGalleryAsync(UserGalleryPhotoDto galleryPhotoDto);
}

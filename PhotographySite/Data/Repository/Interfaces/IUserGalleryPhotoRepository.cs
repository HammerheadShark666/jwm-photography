using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IUserGalleryPhotoRepository : IBaseRepository<UserGalleryPhoto>
{        
    Task<List<Photo>> GetGalleryPhotosAsync(long galleryId);

    Task<UserGalleryPhoto> GetGalleryPhotoAsync(long galleryId, long photoId);

    Task<Photo> GetRandomGalleryPhotoAsync(long galleryId);

	Task<List<UserGalleryPhoto>> GetGalleryPhotosAfterOrderPositionAsync(long galleryId, long photoId, int order);

    Task<List<UserGalleryPhoto>> GetGalleryPhotosBeforeOrderPositionAsync(long galleryId, long photoId, int order);
}
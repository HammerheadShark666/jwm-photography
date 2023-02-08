using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IGalleryPhotoRepository : IBaseRepository<GalleryPhoto>
{        
    Task<List<Photo>> GetGalleryPhotosAsync(long galleryId);

    Task<GalleryPhoto> GetGalleryPhotoAsync(long galleryId, long photoId);

    Task<Photo> GetRandomGalleryPhotoAsync(long galleryId);

	Task<List<GalleryPhoto>> GetGalleryPhotosAfterOrderPositionAsync(long galleryId, long photoId, int order);

    Task<List<GalleryPhoto>> GetGalleryPhotosBeforeOrderPositionAsync(long galleryId, long photoId, int order);
}

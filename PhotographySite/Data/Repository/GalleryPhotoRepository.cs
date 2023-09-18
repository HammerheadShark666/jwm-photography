using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class GalleryPhotoRepository : IGalleryPhotoRepository
{
    private readonly PhotographySiteDbContext _context;

    public GalleryPhotoRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Photo>> GetGalleryPhotosAsync(long galleryId)
    {
        return await (from galleryPhoto in _context.GalleryPhoto
                      join photo in _context.Photo 
                            on galleryPhoto.PhotoId equals photo.Id
                      where galleryPhoto.GalleryId == galleryId
                      orderby(galleryPhoto.Order)
                      select photo).ToListAsync();
    }

    public async Task<GalleryPhoto> GetGalleryPhotoAsync(long galleryId, long photoId)
    {
        return await (from galleryPhoto in _context.GalleryPhoto                         
                      where galleryPhoto.GalleryId == galleryId 
                        && galleryPhoto.PhotoId == photoId                      
                      select galleryPhoto).SingleOrDefaultAsync();
    }

    public async Task<List<GalleryPhoto>> GetGalleryPhotosAfterOrderPositionAsync(long galleryId, long photoId, int order)
    {
        return await (from galleryPhotos in _context.GalleryPhoto                           
                      where galleryPhotos.GalleryId == galleryId
                        && galleryPhotos.PhotoId != photoId
                            && galleryPhotos.Order >= order
                      orderby (galleryPhotos.Order)
                      select galleryPhotos).ToListAsync();
    }

    public async Task<List<GalleryPhoto>> GetGalleryPhotosBeforeOrderPositionAsync(long galleryId, long photoId, int order)
    {
        return await (from galleryPhotos in _context.GalleryPhoto
                      where galleryPhotos.GalleryId == galleryId
                        && galleryPhotos.PhotoId != photoId
                            && galleryPhotos.Order <= order
                      orderby (galleryPhotos.Order)
                      select galleryPhotos).ToListAsync();
    }

    public async Task<Photo> GetRandomGalleryPhotoAsync(long galleryId)
    {
        int gallerySize = await (from galleryPhoto in _context.GalleryPhoto
                                 where galleryPhoto.GalleryId == galleryId
                                    && galleryPhoto.Photo.Orientation == (int)Enums.PhotoOrientation.landscape
                                 select galleryPhoto).CountAsync();

        if(gallerySize == 0) return null;

		Random rand = new Random();
		int toSkip = rand.Next(0, gallerySize);

		return await (from galleryPhoto in _context.GalleryPhoto
				join photo in _context.Photo
					  on galleryPhoto.PhotoId equals photo.Id
				where galleryPhoto.GalleryId == galleryId
                    && galleryPhoto.Photo.Orientation == (int)Enums.PhotoOrientation.landscape
                orderby (galleryPhoto.Order)
				select photo).Skip(toSkip).FirstOrDefaultAsync(); 
	}

    public async Task<GalleryPhoto> AddAsync(GalleryPhoto galleryPhoto)
    {
        await _context.GalleryPhoto.AddAsync(galleryPhoto);
        return galleryPhoto;
    }

    public void Update(GalleryPhoto galleryPhoto)
    {
        _context.GalleryPhoto.Update(galleryPhoto);
    }

    public void Delete(GalleryPhoto galleryPhoto)
    {
        _context.GalleryPhoto.Remove(galleryPhoto);
    }
}
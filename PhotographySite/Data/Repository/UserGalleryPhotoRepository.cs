using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class UserGalleryPhotoRepository(PhotographySiteDbContext context) : IUserGalleryPhotoRepository
{
    public async Task<List<Photo>> GetGalleryPhotosAsync(long galleryId)
    {
        return await (from galleryPhoto in context.UserGalleryPhoto
                      join photo in context.Photo
                            on galleryPhoto.PhotoId equals photo.Id
                      where galleryPhoto.UserGalleryId == galleryId
                      orderby (galleryPhoto.Order)
                      select photo).ToListAsync();
    }

    public async Task<UserGalleryPhoto> GetGalleryPhotoAsync(long galleryId, long photoId)
    {
        return await (from galleryPhoto in context.UserGalleryPhoto
                      where galleryPhoto.UserGalleryId == galleryId
                        && galleryPhoto.PhotoId == photoId
                      select galleryPhoto).SingleOrDefaultAsync();
    }

    public async Task<List<UserGalleryPhoto>> GetGalleryPhotosAfterOrderPositionAsync(long galleryId, long photoId, int order)
    {
        return await (from galleryPhotos in context.UserGalleryPhoto
                      where galleryPhotos.UserGalleryId == galleryId
                        && galleryPhotos.PhotoId != photoId
                            && galleryPhotos.Order >= order
                      orderby (galleryPhotos.Order)
                      select galleryPhotos).ToListAsync();
    }

    public async Task<List<UserGalleryPhoto>> GetGalleryPhotosBeforeOrderPositionAsync(long galleryId, long photoId, int order)
    {
        return await (from galleryPhotos in context.UserGalleryPhoto
                      where galleryPhotos.UserGalleryId == galleryId
                        && galleryPhotos.PhotoId != photoId
                            && galleryPhotos.Order <= order
                      orderby (galleryPhotos.Order)
                      select galleryPhotos).ToListAsync();
    }

    public async Task<Photo> GetRandomGalleryPhotoAsync(long galleryId)
    {
        int gallerySize = await (from galleryPhoto in context.UserGalleryPhoto
                                 where galleryPhoto.UserGalleryId == galleryId
                                    && galleryPhoto.Photo.Orientation == (int)Enums.PhotoOrientation.landscape
                                 select galleryPhoto).CountAsync();

        if (gallerySize == 0) return null;

        Random rand = new();
        int toSkip = rand.Next(0, gallerySize);

        return await (from galleryPhoto in context.UserGalleryPhoto
                      join photo in context.Photo
                      on galleryPhoto.PhotoId equals photo.Id
                      where galleryPhoto.UserGalleryId == galleryId
                          && galleryPhoto.Photo.Orientation == (int)Enums.PhotoOrientation.landscape
                      orderby (galleryPhoto.Order)
                      select photo).Skip(toSkip).FirstOrDefaultAsync();
    }

    public async Task<UserGalleryPhoto> AddAsync(UserGalleryPhoto userGalleryPhoto)
    {
        await context.UserGalleryPhoto.AddAsync(userGalleryPhoto);
        return userGalleryPhoto;
    }

    public void Delete(UserGalleryPhoto userGalleryPhoto)
    {
        context.UserGalleryPhoto.Remove(userGalleryPhoto);
    }
}
using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services;

public class GalleryPhotoService(IUnitOfWork unitOfWork, IMapper mapper) : IGalleryPhotoService
{
    public async Task<List<PhotoResponse>> GetGalleryPhotosAsync(long id)
    {
        return mapper.Map<List<PhotoResponse>>(await unitOfWork.GalleryPhotos.GetGalleryPhotosAsync(id));
    }

    public async Task<GalleryPhoto> AddPhotoToGalleryAsync(GalleryPhotoAddRequest galleryPhotoAddRequest)
    {
        var galleryPhoto = mapper.Map<GalleryPhoto>(galleryPhotoAddRequest);
        galleryPhoto = await unitOfWork.GalleryPhotos.AddAsync(galleryPhoto);

        await UpdatePhotosOrderAsync(galleryPhoto, (galleryPhoto.Order + 1));
        await unitOfWork.Complete();

        return galleryPhoto;
    }

    public async Task<GalleryPhoto> MovePhotoInGalleryAsync(GalleryPhotoAddRequest galleryPhotoAddRequest)
    {
        var currentGalleryPhoto = await unitOfWork.GalleryPhotos.GetGalleryPhotoAsync(galleryPhotoAddRequest.GalleryId, galleryPhotoAddRequest.PhotoId);
        if (currentGalleryPhoto != null)
        {
            int newOrder = galleryPhotoAddRequest.Order;
            await UpdatePhotosAfterMovingPhotosAsync(currentGalleryPhoto, newOrder);
            currentGalleryPhoto.Order = newOrder;
            await unitOfWork.Complete();
        }
        return currentGalleryPhoto;
    }

    public async Task RemovePhotoFromGalleryAsync(GalleryPhotoAddRequest galleryPhotoAddRequest)
    {
        var currentGalleryPhoto = await unitOfWork.GalleryPhotos.GetGalleryPhotoAsync(galleryPhotoAddRequest.GalleryId, galleryPhotoAddRequest.PhotoId);
        if (currentGalleryPhoto != null)
        {
            unitOfWork.GalleryPhotos.Delete(currentGalleryPhoto);
            await UpdatePhotosOrderAsync(currentGalleryPhoto, currentGalleryPhoto.Order);
            await unitOfWork.Complete();
        }

        return;
    }

    private async Task UpdatePhotosOrderAsync(GalleryPhoto galleryPhoto, int newOrder)
    {
        var galleryPhotosAfterOrderPosition = await unitOfWork.GalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.GalleryId, galleryPhoto.PhotoId, galleryPhoto.Order);

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }
    }

    private async Task UpdatePhotosAfterMovingPhotosAsync(GalleryPhoto galleryPhoto, int newOrder)
    {
        if (newOrder > galleryPhoto.Order)
            await MovePhotoInGalleryForwardAsync(galleryPhoto, newOrder);
        else if (newOrder < galleryPhoto.Order)
            await MovePhotoInGalleryBackwardAsync(galleryPhoto, newOrder);
    }

    private async Task MovePhotoInGalleryForwardAsync(GalleryPhoto galleryPhoto, int newOrder)
    {
        int order = 1;

        var galleryPhotosBeforeOrderPosition = await unitOfWork.GalleryPhotos.GetGalleryPhotosBeforeOrderPositionAsync(galleryPhoto.GalleryId, galleryPhoto.PhotoId, newOrder);

        foreach (var galleryPhotoBeforeOrderPosition in galleryPhotosBeforeOrderPosition)
        {
            galleryPhotoBeforeOrderPosition.Order = order;
            order++;
        }

        return;
    }

    private async Task MovePhotoInGalleryBackwardAsync(GalleryPhoto galleryPhoto, int newOrder)
    {
        var galleryPhotosAfterOrderPosition = await unitOfWork.GalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.GalleryId, galleryPhoto.PhotoId, newOrder);

        newOrder++; ;

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }

        return;
    }
}
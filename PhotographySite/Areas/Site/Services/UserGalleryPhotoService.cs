using AutoMapper;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Areas.Site.Services;

public class UserGalleryPhotoService : IUserGalleryPhotoService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public UserGalleryPhotoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 

    public async Task<List<PhotoResponse>> GetGalleryPhotosAsync(long id)
    {
        return _mapper.Map<List<PhotoResponse>>(await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosAsync(id));
    }

    public async Task<UserGalleryPhoto> AddPhotoToUserGalleryAsync(UserGalleryPhotoRequest userGalleryPhotoRequest)
    {
        var galleryPhoto = _mapper.Map<UserGalleryPhoto>(userGalleryPhotoRequest);  

        galleryPhoto = await _unitOfWork.UserGalleryPhotos.AddAsync(galleryPhoto);
        await UpdatePhotosOrderAsync(galleryPhoto, (galleryPhoto.Order + 1));
        await _unitOfWork.Complete();

        return galleryPhoto;
    }

    public async Task<UserGalleryPhoto> MovePhotoInGalleryAsync(UserGalleryMovePhotoRequest userGalleryMovePhotoRequest)
    {
        var currentGalleryPhoto = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotoAsync(userGalleryMovePhotoRequest.UserGalleryId, userGalleryMovePhotoRequest.PhotoId);
        
        if(currentGalleryPhoto != null)
        {
            int newOrder = userGalleryMovePhotoRequest.Order; 
            await UpdatePhotosAfterMovingPhotosAsync(currentGalleryPhoto, newOrder);
            currentGalleryPhoto.Order = newOrder;
            await _unitOfWork.Complete();
        }

        return currentGalleryPhoto;
    }

    public async Task RemovePhotoFromGalleryAsync(long userGalleryId, long photoId)
    {
        var currentGalleryPhoto = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotoAsync(userGalleryId, photoId);
        
        if (currentGalleryPhoto != null)
        {           
            _unitOfWork.UserGalleryPhotos.Delete(currentGalleryPhoto);
            await UpdatePhotosOrderAsync(currentGalleryPhoto, currentGalleryPhoto.Order);
            await _unitOfWork.Complete();
        }
        return;
    } 

    private async Task UpdatePhotosOrderAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        var galleryPhotosAfterOrderPosition = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.UserGalleryId, galleryPhoto.PhotoId, galleryPhoto.Order);

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }
    }
     
    private async Task UpdatePhotosAfterMovingPhotosAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        if (newOrder > galleryPhoto.Order)         
            await MovePhotoInGalleryForwardAsync(galleryPhoto, newOrder);  
        else if (newOrder < galleryPhoto.Order)
            await MovePhotoInGalleryBackwardAsync(galleryPhoto, newOrder);
    }

    private async Task MovePhotoInGalleryForwardAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        int order = 1;

        var galleryPhotosBeforeOrderPosition = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosBeforeOrderPositionAsync(galleryPhoto.UserGalleryId, galleryPhoto.PhotoId, newOrder);

        foreach (var galleryPhotoBeforeOrderPosition in galleryPhotosBeforeOrderPosition)
        {
            galleryPhotoBeforeOrderPosition.Order = order;
            order++;
        }

        return;
    }

    private async Task MovePhotoInGalleryBackwardAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        var galleryPhotosAfterOrderPosition = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.UserGalleryId, galleryPhoto.PhotoId, newOrder);

        newOrder++; ;

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }

        return;
    }
}
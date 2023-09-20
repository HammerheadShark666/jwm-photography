using AutoMapper;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using PhotographySite.Models;
using SwanSong.Service.Helpers.Exceptions;

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

    public async Task<List<PhotoResponse>> GetGalleryPhotosAsync(Guid userId, long galleryId)
    {
        await UserGalleryExists(userId, galleryId);

        //if (!await UserGalleryExists(userId, galleryId))
        //    throw new UserGalleryNotFoundException("User Gallary not found.");

        return _mapper.Map<List<PhotoResponse>>(await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosAsync(galleryId));
    }

    public async Task<UserGalleryPhoto> AddPhotoToUserGalleryAsync(UserGalleryPhotoRequest request)
    {
        await UserGalleryExists(request.UserId, request.UserGalleryId);

        //if (!await UserGalleryExists(userGalleryPhotoRequest.UserId, userGalleryPhotoRequest.UserGalleryId))
        //    throw new UserGalleryNotFoundException("User Gallary not found.");

        var galleryPhoto = _mapper.Map<UserGalleryPhoto>(request);  

        galleryPhoto = await _unitOfWork.UserGalleryPhotos.AddAsync(galleryPhoto);
        await UpdatePhotosOrderAsync(galleryPhoto, (galleryPhoto.Order + 1));
        await _unitOfWork.Complete();

        return galleryPhoto;
    }

    public async Task<UserGalleryPhoto> MovePhotoInGalleryAsync(UserGalleryMovePhotoRequest request)
    {
        await UserGalleryExists(request.UserId, request.UserGalleryId);

        //if (!await UserGalleryExists(userGalleryMovePhotoRequest.UserId, userGalleryMovePhotoRequest.UserGalleryId))
        //    throw new UserGalleryNotFoundException("User Gallary not found.");

        var currentGalleryPhoto = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotoAsync(request.UserGalleryId, request.PhotoId);
        
        if(currentGalleryPhoto != null)
        {
            int newOrder = request.Order; 
            await UpdatePhotosAfterMovingPhotosAsync(currentGalleryPhoto, newOrder);
            currentGalleryPhoto.Order = newOrder;
            await _unitOfWork.Complete();
        }

        return currentGalleryPhoto;
    }

    public async Task RemovePhotoFromGalleryAsync(UserGalleryRemoveRequest request)
    {
        await UserGalleryExists(request.UserId, request.UserGalleryId);

        //if (!await UserGalleryExists(userGalleryRemoveRequest.UserId, userGalleryRemoveRequest.UserGalleryId))
        //    throw new UserGalleryNotFoundException("User Gallary not found.");

        var currentGalleryPhoto = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotoAsync(request.UserGalleryId, request.PhotoId);
        
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

    private async Task UserGalleryExists(Guid userId, long id)
    {
      //  return await _unitOfWork.UserGalleries.GetAsync(userId, id) != null;

        if (!(await _unitOfWork.UserGalleries.GetAsync(userId, id) != null))
            throw new UserGalleryNotFoundException("User Gallary not found.");

        return;
    }
}
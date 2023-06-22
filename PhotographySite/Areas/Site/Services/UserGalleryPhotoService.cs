using AutoMapper;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

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

    public async Task<List<PhotoDto>> GetGalleryPhotosAsync(long id)
    {
        return _mapper.Map<List<PhotoDto>>(await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosAsync(id));
    }

    public async Task<UserGalleryPhoto> AddPhotoToUserGalleryAsync(UserGalleryPhotoDto galleryPhotoDto)  //long galleryId, long photoId, int order)
    {
        UserGalleryPhoto galleryPhoto = _mapper.Map<UserGalleryPhoto>(galleryPhotoDto);  

        galleryPhoto = await _unitOfWork.UserGalleryPhotos.AddAsync(galleryPhoto);
        await UpdatePhotosOrderAsync(galleryPhoto, (galleryPhoto.Order + 1));       
        _unitOfWork.Complete();

        return galleryPhoto;
    }

    public async Task<UserGalleryPhoto> MovePhotoInGalleryAsync(UserGalleryPhotoDto galleryPhotoDto)
    {
        UserGalleryPhoto currentGalleryPhoto = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotoAsync(galleryPhotoDto.UserGalleryId, galleryPhotoDto.PhotoId);
        
        if(currentGalleryPhoto != null)
        {
            int newOrder = galleryPhotoDto.Order; 
            await UpdatePhotosAfterMovingPhotosAsync(currentGalleryPhoto, newOrder);
            currentGalleryPhoto.Order = newOrder;
            _unitOfWork.Complete();
        }

        return currentGalleryPhoto;
    }

    public async Task RemovePhotoFromGalleryAsync(UserGalleryPhotoDto galleryPhotoDto)
    {
        UserGalleryPhoto currentGalleryPhoto = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotoAsync(galleryPhotoDto.UserGalleryId, galleryPhotoDto.PhotoId);
        
        if (currentGalleryPhoto != null)
        {           
            _unitOfWork.UserGalleryPhotos.Remove(currentGalleryPhoto);
            await UpdatePhotosOrderAsync(currentGalleryPhoto, currentGalleryPhoto.Order);
            _unitOfWork.Complete();
        }
        return;
    } 

    private async Task UpdatePhotosOrderAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        List<UserGalleryPhoto> galleryPhotosAfterOrderPosition = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.UserGalleryId, galleryPhoto.PhotoId, galleryPhoto.Order);

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }
    }
     
    private async Task UpdatePhotosAfterMovingPhotosAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        if (newOrder > galleryPhoto.Order)
        {
            await MovePhotoInGalleryForwardAsync(galleryPhoto, newOrder);             
        }
        else if (newOrder < galleryPhoto.Order)
        {
            await MovePhotoInGalleryBackwardAsync(galleryPhoto, newOrder);
        }
    }

    private async Task MovePhotoInGalleryForwardAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        int order = 1;

        List<UserGalleryPhoto> galleryPhotosBeforeOrderPosition = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosBeforeOrderPositionAsync(galleryPhoto.UserGalleryId, galleryPhoto.PhotoId, newOrder);

        foreach (var galleryPhotoBeforeOrderPosition in galleryPhotosBeforeOrderPosition)
        {
            galleryPhotoBeforeOrderPosition.Order = order;
            order++;
        }

        return;
    }

    private async Task MovePhotoInGalleryBackwardAsync(UserGalleryPhoto galleryPhoto, int newOrder)
    {
        List<UserGalleryPhoto> galleryPhotosAfterOrderPosition = await _unitOfWork.UserGalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.UserGalleryId, galleryPhoto.PhotoId, newOrder);

        newOrder++; ;

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }

        return;
    }
}
using AutoMapper;
using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services;

public class GalleryPhotoService : IGalleryPhotoService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GalleryPhotoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 

    public async Task<List<PhotoDto>> GetGalleryPhotosAsync(long id)
    {
        return _mapper.Map<List<PhotoDto>>(await _unitOfWork.GalleryPhotos.GetGalleryPhotosAsync(id));
    }

    public async Task<GalleryPhoto> AddPhotoToGalleryAsync(GalleryPhotoDto galleryPhotoDto)  //long galleryId, long photoId, int order)
    { 
        GalleryPhoto galleryPhoto = _mapper.Map<GalleryPhoto>(galleryPhotoDto);

        galleryPhoto = await _unitOfWork.GalleryPhotos.AddAsync(galleryPhoto);
        await UpdatePhotosOrderAsync(galleryPhoto, (galleryPhoto.Order + 1));       
        _unitOfWork.Complete();

        return galleryPhoto;
    }

    public async Task<GalleryPhoto> MovePhotoInGalleryAsync(GalleryPhotoDto galleryPhotoDto)
    { 
        GalleryPhoto currentGalleryPhoto = await _unitOfWork.GalleryPhotos.GetGalleryPhotoAsync(galleryPhotoDto.GalleryId, galleryPhotoDto.PhotoId);
        if(currentGalleryPhoto != null)
        {
            int newOrder = galleryPhotoDto.Order; 
            await UpdatePhotosAfterMovingPhotosAsync(currentGalleryPhoto, newOrder);
            currentGalleryPhoto.Order = newOrder;
            _unitOfWork.Complete();
        }

        return currentGalleryPhoto;
    }

    public async Task RemovePhotoFromGalleryAsync(GalleryPhotoDto galleryPhotoDto)
    {
        GalleryPhoto currentGalleryPhoto = await _unitOfWork.GalleryPhotos.GetGalleryPhotoAsync(galleryPhotoDto.GalleryId, galleryPhotoDto.PhotoId);
        if (currentGalleryPhoto != null)
        {           
            _unitOfWork.GalleryPhotos.Remove(currentGalleryPhoto);
            await UpdatePhotosOrderAsync(currentGalleryPhoto, currentGalleryPhoto.Order);
            _unitOfWork.Complete();
        }
        return;
    } 

    private async Task UpdatePhotosOrderAsync(GalleryPhoto galleryPhoto, int newOrder)
    {
        List<GalleryPhoto> galleryPhotosAfterOrderPosition = await _unitOfWork.GalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.GalleryId, galleryPhoto.PhotoId, galleryPhoto.Order);

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }
    }
     
    private async Task UpdatePhotosAfterMovingPhotosAsync(GalleryPhoto galleryPhoto, int newOrder)
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

    private async Task MovePhotoInGalleryForwardAsync(GalleryPhoto galleryPhoto, int newOrder)
    {
        int order = 1;

        List<GalleryPhoto> galleryPhotosBeforeOrderPosition = await _unitOfWork.GalleryPhotos.GetGalleryPhotosBeforeOrderPositionAsync(galleryPhoto.GalleryId, galleryPhoto.PhotoId, newOrder);

        foreach (var galleryPhotoBeforeOrderPosition in galleryPhotosBeforeOrderPosition)
        {
            galleryPhotoBeforeOrderPosition.Order = order;
            order++;
        }

        return;
    }

    private async Task MovePhotoInGalleryBackwardAsync(GalleryPhoto galleryPhoto, int newOrder)
    {
        List<GalleryPhoto> galleryPhotosAfterOrderPosition = await _unitOfWork.GalleryPhotos.GetGalleryPhotosAfterOrderPositionAsync(galleryPhoto.GalleryId, galleryPhoto.PhotoId, newOrder);

        newOrder++; ;

        foreach (var galleryPhotoAfterOrderPosition in galleryPhotosAfterOrderPosition)
        {
            galleryPhotoAfterOrderPosition.Order = newOrder;
            newOrder++;
        }

        return;
    }
}
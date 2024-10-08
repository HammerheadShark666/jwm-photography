﻿using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IUserGalleryPhotoService
{
    Task<List<PhotoResponse>> GetGalleryPhotosAsync(Guid userId, long galleryId);
    Task<UserGalleryPhoto> AddPhotoToUserGalleryAsync(UserGalleryPhotoRequest userGalleryPhotoRequest);
    Task<UserGalleryPhoto> MovePhotoInGalleryAsync(UserGalleryMovePhotoRequest userGalleryMovePhotoRequest);
    Task RemovePhotoFromGalleryAsync(UserGalleryRemoveRequest userGalleryRemoveRequest);
}
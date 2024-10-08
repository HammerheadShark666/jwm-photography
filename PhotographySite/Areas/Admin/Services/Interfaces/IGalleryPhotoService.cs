﻿using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IGalleryPhotoService
{
    Task<List<PhotoResponse>> GetGalleryPhotosAsync(long id);
    Task<GalleryPhoto> AddPhotoToGalleryAsync(GalleryPhotoAddRequest galleryPhotoAddRequest);
    Task<GalleryPhoto> MovePhotoInGalleryAsync(GalleryPhotoAddRequest galleryPhotoAddRequest);
    Task RemovePhotoFromGalleryAsync(GalleryPhotoAddRequest galleryPhotoAddRequest);
}
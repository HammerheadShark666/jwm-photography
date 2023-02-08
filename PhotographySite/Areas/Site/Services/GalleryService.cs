using AutoMapper;
using FluentValidation;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Areas.Site.Services;

public class GalleryService : IGalleryService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IValidator<Gallery> _galleryValidator;

    public GalleryService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<Gallery> galleryValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _galleryValidator = galleryValidator;
    }

    public async Task<GalleryDto> GetGalleryAsync(long id)
    {
        return _mapper.Map<GalleryDto>(await _unitOfWork.Galleries.GetFullGalleryAsync(id));
       // return _mapper.Map<GalleryDto>(await _unitOfWork.Galleries.ByIdAsync(id));
    }

    public async Task<List<GalleryDto>> GetGalleriesAsync()
    {
		List<GalleryDto> galleries = _mapper.Map<List<GalleryDto>>(await _unitOfWork.Galleries.AllSortedAsync());

        foreach(GalleryDto gallery in galleries)
        {
            Photo randomPhoto = (await _unitOfWork.GalleryPhotos.GetRandomGalleryPhotoAsync(gallery.Id));

            if(randomPhoto != null)
			    gallery.RandomPhoto = randomPhoto.FileName;	
		}

        return galleries;
    } 
}

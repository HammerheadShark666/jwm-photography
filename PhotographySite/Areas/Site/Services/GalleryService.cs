using AutoMapper;
using FluentValidation;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
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

    public async Task<GalleryResponse> GetGalleryAsync(long id)
    {  
		return _mapper.Map<GalleryResponse>(await _unitOfWork.Galleries.GetFullGalleryAsync(id)); 
	}

    public async Task<GalleryResponse> GetGalleryAsync(Guid userId, long id)
    { 
        return _mapper.Map<GalleryResponse>(await _unitOfWork.Galleries.GetFullGalleryAsync(userId, id));
    }

    public async Task<GalleriesResponse> GetGalleriesAsync()
    {
		var galleries = _mapper.Map<List<GalleryResponse>>(await _unitOfWork.Galleries.AllSortedAsync());

        foreach(GalleryResponse gallery in galleries)
        {
            var randomPhoto = (await _unitOfWork.GalleryPhotos.GetRandomGalleryPhotoAsync(gallery.Id));
            if(randomPhoto != null)
			    gallery.RandomPhoto = randomPhoto.FileName;	 
		}
  
        return new GalleriesResponse() { Galleries = galleries };
    } 
}
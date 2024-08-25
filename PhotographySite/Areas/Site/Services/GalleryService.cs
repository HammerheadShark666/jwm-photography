using AutoMapper;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using SwanSong.Service.Helpers.Exceptions;

namespace PhotographySite.Areas.Site.Services;

public class GalleryService : IGalleryService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public GalleryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GalleryResponse> GetGalleryAsync(long id)
    {
        if (!await GalleryExists(id))
            throw new GalleryNotFoundException("Gallary not found.");

        return _mapper.Map<GalleryResponse>(await _unitOfWork.Galleries.GetFullGalleryAsync(id));
    }

    public async Task<GalleryResponse> GetGalleryAsync(Guid userId, long id)
    {
        if (!await GalleryExists(id))
            throw new GalleryNotFoundException("Gallary not found.");

        return _mapper.Map<GalleryResponse>(await _unitOfWork.Galleries.GetFullGalleryAsync(userId, id));
    }

    public async Task<GalleriesResponse> GetGalleriesAsync()
    {
        var galleries = _mapper.Map<List<GalleryResponse>>(await _unitOfWork.Galleries.AllSortedAsync());

        foreach (GalleryResponse gallery in galleries)
        {
            var randomPhoto = (await _unitOfWork.GalleryPhotos.GetRandomGalleryPhotoAsync(gallery.Id));
            if (randomPhoto != null)
                gallery.RandomPhoto = randomPhoto.FileName;
        }

        return new GalleriesResponse() { Galleries = galleries };
    }

    private async Task<bool> GalleryExists(long id)
    {
        return await _unitOfWork.Galleries.ByIdAsync(id) != null;
    }
}
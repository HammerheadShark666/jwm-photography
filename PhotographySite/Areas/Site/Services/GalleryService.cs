using AutoMapper;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Response;
using SwanSong.Service.Helpers.Exceptions;

namespace PhotographySite.Areas.Site.Services;

public class GalleryService(IUnitOfWork unitOfWork, IMapper mapper) : IGalleryService
{
    public async Task<GalleryResponse> GetGalleryAsync(long id)
    {
        if (!await GalleryExists(id))
            throw new GalleryNotFoundException("Gallary not found.");

        return mapper.Map<GalleryResponse>(await unitOfWork.Galleries.GetFullGalleryAsync(id));
    }

    public async Task<GalleryResponse> GetGalleryAsync(Guid userId, long id)
    {
        if (!await GalleryExists(id))
            throw new GalleryNotFoundException("Gallary not found.");

        return mapper.Map<GalleryResponse>(await unitOfWork.Galleries.GetFullGalleryAsync(userId, id));
    }

    public async Task<GalleriesResponse> GetGalleriesAsync()
    {
        var galleries = mapper.Map<List<GalleryResponse>>(await unitOfWork.Galleries.AllSortedAsync());

        foreach (GalleryResponse gallery in galleries)
        {
            var randomPhoto = (await unitOfWork.GalleryPhotos.GetRandomGalleryPhotoAsync(gallery.Id));
            if (randomPhoto != null)
                gallery.RandomPhoto = randomPhoto.FileName;
        }

        return new GalleriesResponse() { Galleries = galleries };
    }

    private async Task<bool> GalleryExists(long id)
    {
        return await unitOfWork.Galleries.ByIdAsync(id) != null;
    }
}
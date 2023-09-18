using PhotographySite.Data.Repository.Interfaces;

namespace PhotographySite.Data.UnitOfWork.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IPhotoRepository Photos { get; } 
    ICountryRepository Countries { get; }
    ICategoryRepository Categories { get; }
    IMontageRepository Montages { get; }
    IPaletteRepository Palettes { get; }
    IGalleryRepository Galleries { get; } 
    IGalleryPhotoRepository GalleryPhotos { get; }
    IUserGalleryRepository UserGalleries { get; }
    IUserGalleryPhotoRepository UserGalleryPhotos { get; }
    IFavouriteRepository Favourites { get; }
    IUserRepository Users { get; }
    Task<int> Complete();
}
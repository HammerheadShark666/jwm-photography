using PhotographySite.Data.Repository;
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

    IFavouriteRepository Favourites { get; }

    IGalleryPhotoRepository GalleryPhotos { get; }

    IUserRepository Users { get; }

    int Complete();
}
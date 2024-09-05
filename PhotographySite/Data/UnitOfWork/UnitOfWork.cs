using PhotographySite.Data.Context;
using PhotographySite.Data.Repository;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly PhotographySiteDbContext _context;

    public IPhotoRepository Photos { get; private set; }
    public ICountryRepository Countries { get; private set; }
    public ICategoryRepository Categories { get; private set; }
    public IPaletteRepository Palettes { get; private set; }
    public IMontageRepository Montages { get; private set; }
    public IGalleryRepository Galleries { get; private set; }
    public IGalleryPhotoRepository GalleryPhotos { get; private set; }
    public IUserGalleryRepository UserGalleries { get; private set; }
    public IUserGalleryPhotoRepository UserGalleryPhotos { get; private set; }
    public IFavouriteRepository Favourites { get; private set; }
    public IUserRepository Users { get; private set; }

    public UnitOfWork(PhotographySiteDbContext context)
    {
        _context = context;
        Photos = new PhotoRepository(_context);
        Countries = new CountryRepository(_context);
        Categories = new CategoryRepository(_context);
        Montages = new MontageRepository(_context);
        Palettes = new PaletteRepository(_context);
        Galleries = new GalleryRepository(_context);
        GalleryPhotos = new GalleryPhotoRepository(_context);
        Favourites = new FavouriteRepository(_context);
        Users = new UserRepository(_context);
        UserGalleries = new UserGalleryRepository(_context);
        UserGalleryPhotos = new UserGalleryPhotoRepository(_context);
    }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
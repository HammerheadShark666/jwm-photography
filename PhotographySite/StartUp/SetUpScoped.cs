using PhotographySite.Areas.Admin.Services;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Services;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers.Interface;
using PhotographySite.Services;
using PhotographySite.Services.Interfaces;
using SwanSong.Helper;

namespace PhotographySite.StartUp;

public class SetUpScoped
{
    public static void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAzureStorageBlobHelper, AzureStorageBlobHelper>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<Areas.Admin.Services.Interfaces.IMontageService, Areas.Admin.Services.MontageService> ();
        builder.Services.AddScoped<Areas.Site.Services.Interfaces.IMontageService, Areas.Site.Services.MontageService>();
        builder.Services.AddScoped<IPhotoCatalogService, PhotoCatalogService>();
        builder.Services.AddScoped<IPhotoImportService, PhotoImportService>();
        builder.Services.AddScoped<Areas.Admin.Services.Interfaces.IGalleryService, Areas.Admin.Services.GalleryService>();
        builder.Services.AddScoped<Areas.Site.Services.Interfaces.IGalleryService, Areas.Site.Services.GalleryService>();
        builder.Services.AddScoped<IGalleryPhotoService, GalleryPhotoService>();
		builder.Services.AddScoped<ICountryService, CountryService>();
		builder.Services.AddScoped<ICategoryService, CategoryService>();
		builder.Services.AddScoped<IPaletteService, PaletteService>();
        builder.Services.AddScoped<IFavouriteService, FavouriteService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<PhotographySite.Areas.Site.Services.Interfaces.IMontageService, PhotographySite.Areas.Site.Services.MontageService>();
		builder.Services.AddScoped<PhotographySite.Areas.Site.Services.Interfaces.IGalleryService, PhotographySite.Areas.Site.Services.GalleryService>() ;
	}
}
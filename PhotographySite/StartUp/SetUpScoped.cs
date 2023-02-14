using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Data.UnitOfWork;
using PhotographySite.Areas.Admin.Services;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Helpers.Interface;
using SwanSong.Helper;

namespace PhotographySite.StartUp;

public class SetUpScoped
{
    public static void Setup(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAzureStorageBlobHelper, AzureStorageBlobHelper>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IMontageService, MontageService > ();
        builder.Services.AddScoped<IPhotoCatalogService, PhotoCatalogService>();
        builder.Services.AddScoped<IPhotoImportService, PhotoImportService>();
        builder.Services.AddScoped<IGalleryService, GalleryService>();
        builder.Services.AddScoped<IGalleryPhotoService, GalleryPhotoService>();
		builder.Services.AddScoped<ICountryService, CountryService>();
		builder.Services.AddScoped<ICategoryService, CategoryService>();
		builder.Services.AddScoped<IPaletteService, PaletteService>();
		builder.Services.AddScoped<PhotographySite.Areas.Site.Services.Interfaces.IMontageService, PhotographySite.Areas.Site.Services.MontageService>();
		builder.Services.AddScoped<PhotographySite.Areas.Site.Services.Interfaces.IGalleryService, PhotographySite.Areas.Site.Services.GalleryService>() ;
	}
}
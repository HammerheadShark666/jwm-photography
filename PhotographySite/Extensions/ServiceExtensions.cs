using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using PhotographySite.Areas.Admin.Services;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Services;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Business;
using PhotographySite.Data.Context;
using PhotographySite.Data.UnitOfWork;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Helpers;
using PhotographySite.Helpers.Interface;
using PhotographySite.Models;
using PhotographySite.Services;
using PhotographySite.Services.Interfaces;
using System.Reflection;

namespace PhotographySite.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureSession(this IServiceCollection services)
    {
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(60);
        });
    }

    public static void ConfigureDatabaseContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<PhotographySiteDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(Constants.DatabaseConnectionString),
            options => options.EnableRetryOnFailure()));
    }

    public static void ConfigureApplicationInsights(this IServiceCollection services)
    {
        services.AddApplicationInsightsTelemetry();
    }

    public static void ConfigureMvc(this IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddMvc(options => options.EnableEndpointRouting = false);
    }

    public static void ConfigureControllers(this IServiceCollection services)
    {
        services.AddControllersWithViews();

        services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
    }

    public static void ConfigureScoped(this IServiceCollection services)
    {
        services.AddScoped<Areas.Admin.Services.Interfaces.IGalleryService, Areas.Admin.Services.GalleryService>();
        services.AddScoped<Areas.Site.Services.Interfaces.IGalleryService, Areas.Site.Services.GalleryService>();
        services.AddScoped<Areas.Admin.Services.Interfaces.IMontageService, Areas.Admin.Services.MontageService>();
        services.AddScoped<Areas.Site.Services.Interfaces.IMontageService, Areas.Site.Services.MontageService>();
        services.AddScoped<IFavouriteService, FavouriteService>();
        services.AddScoped<IPhotoCatalogService, PhotoCatalogService>();
        services.AddScoped<IPhotoImportService, PhotoImportService>();
        services.AddScoped<IUserGalleryService, UserGalleryService>();
        services.AddScoped<IUserGalleryPhotoService, UserGalleryPhotoService>();
        services.AddScoped<IGalleryPhotoService, GalleryPhotoService>();
        services.AddScoped<ICountryService, CountryService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IPaletteService, PaletteService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAzureStorageBlobHelper, AzureStorageBlobHelper>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IValidatorHelper<>), typeof(ValidatorHelper<>));
    }

    public static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetAssembly(typeof(AutoMapperProfile)));
    }

    public static void ConfigureFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<GalleryValidator>();
    }

    public static void ConfigureIdentity(this IServiceCollection services)
    {
        services.Configure<IdentityOptions>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })

        .AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<PhotographySiteDbContext>()
        .AddDefaultTokenProviders();

        services.ConfigureApplicationCookie(options =>
        {
            options.AccessDeniedPath = "/AccessDenied";
            options.Cookie.Name = "jwmPhotography";
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            options.LoginPath = "/login";
            options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            options.SlidingExpiration = true;
        });
    }

    public static void ConfigureResponseCaching(this IServiceCollection services)
    {
        services.AddResponseCaching();
    }
}
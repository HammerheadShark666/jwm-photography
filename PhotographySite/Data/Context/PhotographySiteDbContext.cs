using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotographySite.Models;

namespace PhotographySite.Data.Context;
public class PhotographySiteDbContext(DbContextOptions<PhotographySiteDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Category> Category { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Gallery> Gallery { get; set; }
    public DbSet<GalleryPhoto> GalleryPhoto { get; set; }
    public DbSet<Montage> Montage { get; set; }
    public DbSet<Orientation> Orientation { get; set; }
    public DbSet<Palette> Palette { get; set; }
    public DbSet<Photo> Photo { get; set; }
    public DbSet<Favourite> Favourite { get; set; }
    public DbSet<UserGallery> UserGallery { get; set; }
    public DbSet<UserGalleryPhoto> UserGalleryPhoto { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Favourite>().HasKey(fv => new { fv.UserId, fv.PhotoId });

        modelBuilder.Entity<Category>().HasData(DefaultData.GetCategoryDefaultData());
        modelBuilder.Entity<Country>().HasData(DefaultData.GetCountryDefaultData());
        modelBuilder.Entity<Gallery>().HasData(DefaultData.GetGalleryDefaultData());
        modelBuilder.Entity<Montage>().HasData(DefaultData.GetMontageDefaultData());
        modelBuilder.Entity<Orientation>().HasData(DefaultData.GetOrientationDefaultData());
        modelBuilder.Entity<Palette>().HasData(DefaultData.GetPaletteDefaultData());
    }
}

//EntityFrameworkCore\Add-Migration create-db add-favorite-table
//EntityFrameworkCore\update-database   

//EntityFramework6\Add-Migration
//EntityFramework6\update-database

//dotnet ef migrations add description-column-to-gallery --project PhotographySite
//dotnet ef database update --project PhotographySite

//azurite --silent --location c:\azurite --debug c:\azurite\debug.log
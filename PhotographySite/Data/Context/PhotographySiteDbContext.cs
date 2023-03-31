using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Helpers;
using PhotographySite.Models;

namespace PhotographySite.Data.Contexts;
public class PhotographySiteDbContext : DbContext
{ 
    public PhotographySiteDbContext(DbContextOptions<PhotographySiteDbContext> options) : base(options) { }
 
    public DbSet<Category> Category { get; set; }
    public DbSet<Country> Country { get; set; }
    public DbSet<Gallery> Gallery { get; set; }
    public DbSet<GalleryPhoto> GalleryPhoto { get; set; }
    public DbSet<Montage> Montage { get; set; }
    public DbSet<Orientation> Orientation { get; set; }
    public DbSet<Palette> Palette { get; set; }
    public DbSet<Photo> Photo { get; set; }
    public DbSet<Showcase> Showcase { get; set; }
    public DbSet<ShowcasePhoto> ShowcasePhoto { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);         

        modelBuilder.Entity<Category>().HasData(DefaultData.GetCategoryDefaultData());
        modelBuilder.Entity<Country>().HasData(DefaultData.GetCountryDefaultData());
        modelBuilder.Entity<Gallery>().HasData(DefaultData.GetGalleryDefaultData());
        modelBuilder.Entity<Montage>().HasData(DefaultData.GetMontageDefaultData());
        modelBuilder.Entity<Orientation>().HasData(DefaultData.GetOrientationDefaultData());
        modelBuilder.Entity<Palette>().HasData(DefaultData.GetPaletteDefaultData());         
        modelBuilder.Entity<Showcase>().HasData(DefaultData.GetShowcaseDefaultData()); 
    }
}

//EntityFrameworkCore\Add-Migration create-db
//EntityFrameworkCore\update-database   

//EntityFramework6\Add-Migration
//EntityFramework6\update-database

//dotnet ef migrations add description-column-to-gallery --project PhotographySite
//dotnet ef database update --project PhotographySite

//azurite --silent --location c:\azurite --debug c:\azurite\debug.log
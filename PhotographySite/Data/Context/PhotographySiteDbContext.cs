using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PhotographySite.Data.Contexts;
public class PhotographySiteDbContext : DbContext
{
    public PhotographySiteDbContext(DbContextOptions<PhotographySiteDbContext> options) : base(options)
    {
    }
    
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
        //modelBuilder.Entity<GalleryPhoto>().HasData(DefaultData.GetGalleryPhotoDefaultData());

        modelBuilder.Entity<Montage>().HasData(DefaultData.GetMontageDefaultData());

        modelBuilder.Entity<Orientation>().HasData(DefaultData.GetOrientationDefaultData());
        modelBuilder.Entity<Palette>().HasData(DefaultData.GetPaletteDefaultData());

       // modelBuilder.Entity<Photo>().HasData(DefaultData.GetPhotoDefaultData());
        modelBuilder.Entity<Showcase>().HasData(DefaultData.GetShowcaseDefaultData());
       //modelBuilder.Entity<ShowcasePhoto>().HasData(DefaultData.GetShowcasePhotoDefaultData());        
    }
}


//EntityFrameworkCore\Add-Migration create-db
//EntityFrameworkCore\update-database   
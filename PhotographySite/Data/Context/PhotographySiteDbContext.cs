using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhotographySite.Helpers;

namespace PhotographySite.Data.Contexts;
public class PhotographySiteDbContext : DbContext
{

    //UNCOMMENT OUT WHEN RUNNING FOR MIGRATIONS
    //public PhotographySiteDbContext()
    //{

    //}

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    //optionsBuilder.UseSqlServer(EnvironmentVariablesHelper.DatabaseConnectionString());
    //    optionsBuilder.UseSqlServer("Server=tcp:jwm-photography-db-server.database.windows.net,1433;Initial Catalog=jwm-photography-db;Persist Security Info=False;User ID=JwmPhotographyAdmin;Password=AntiqueRoadtrip66#3;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
    //    // optionsBuilder.UseSqlServer("Initial Catalog=SwanSongDB; Data Source=localhost, 1440; Persist Security Info=True;User ID=SA;Password=Rcu9OP443mc#3xx;");
    //}


    public PhotographySiteDbContext(DbContextOptions<PhotographySiteDbContext> options) : base(options)
    {
        var b = 1;
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



//dotnet ef migrations add description-column-to-gallery --project PhotographySite
//dotnet ef database update --project PhotographySite


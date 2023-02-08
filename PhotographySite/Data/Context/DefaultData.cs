using PhotographySite.Models;

namespace PhotographySite.Data.Context;

public class DefaultData
{
    public static List<Country> GetCountryDefaultData()
    {
        return new List<Country>()
        {
            new Country { Id = 1, Name = "England" },
            new Country { Id = 2, Name = "Scotland" },
            new Country { Id = 3, Name = "Wales" },
            new Country { Id = 5, Name = "India" },
            new Country { Id = 6, Name = "Australia" },
            new Country { Id = 7, Name = "Nepal" },
            new Country { Id = 8, Name = "Tibet" },
            new Country { Id = 9, Name = "China" },
            new Country { Id = 10, Name = "Vietnam" },
            new Country { Id = 11, Name = "Cambodia" },
            new Country { Id = 12, Name = "Thailand" },
            new Country { Id = 13, Name = "Malaysia" },
            new Country { Id = 14, Name = "Borneo" },
            new Country { Id = 15, Name = "Philippines" },
            new Country { Id = 16, Name = "Egypt" },
            new Country { Id = 17, Name = "Indonesia" },
            new Country { Id = 18, Name = "Peru" },
            new Country { Id = 19, Name = "Bolivia" },
            new Country { Id = 20, Name = "Chile" },
            new Country { Id = 21, Name = "Argentina" },
            new Country { Id = 22, Name = "Germany" },
            new Country { Id = 23, Name = "Spain" },
            new Country { Id = 24, Name = "Myanmar" },
		};
    }

    public static List<Category> GetCategoryDefaultData()
    {
        return new List<Category>()
        {
            new Category { Id = 1, Name = "Landscape" },
            new Category { Id = 2, Name = "Travel" },
            new Category { Id = 3, Name = "Wildlife" },
            new Category { Id = 4, Name = "Underwater" },
            new Category { Id = 5, Name = "Portrait" },
            new Category { Id = 6, Name = "Macro" },
            new Category { Id = 7, Name = "Miscellaneous" },
        };
    }

    public static List<Gallery> GetGalleryDefaultData()
    {
        return new List<Gallery>()
        {
            new Gallery { Id = 1, Name = "Landscape" },
            new Gallery { Id = 2, Name = "Travel" },
            new Gallery { Id = 3, Name = "Wildlife" },
            new Gallery { Id = 4, Name = "Underwater" },
            new Gallery { Id = 5, Name = "Portraits" },
            new Gallery { Id = 6, Name = "Black & White" },
            new Gallery { Id = 7, Name = "Macro" }

        };
    }

    public static List<GalleryPhoto> GetGalleryPhotoDefaultData()
    {
        return new List<GalleryPhoto>()
        {
            new GalleryPhoto { Id = 1, GalleryId= 4, PhotoId = 731, Order = 8 },
            new GalleryPhoto { Id = 2, GalleryId= 4, PhotoId = 493, Order = 3 },
            new GalleryPhoto { Id = 3, GalleryId= 4, PhotoId = 496, Order = 7 },
            new GalleryPhoto { Id = 4, GalleryId= 4, PhotoId = 517, Order = 4 },
            new GalleryPhoto { Id = 5, GalleryId= 4, PhotoId = 734, Order = 1 },
            new GalleryPhoto { Id = 6, GalleryId= 4, PhotoId = 510, Order = 2 },
            new GalleryPhoto { Id = 7, GalleryId= 4, PhotoId = 495, Order = 5 },
            new GalleryPhoto { Id = 8, GalleryId= 4, PhotoId = 509, Order = 6 },
            new GalleryPhoto { Id = 9, GalleryId= 1, PhotoId = 837, Order = 2 },
            new GalleryPhoto { Id = 10, GalleryId= 1, PhotoId = 30, Order = 1 },
            new GalleryPhoto { Id = 11, GalleryId= 1, PhotoId = 832, Order = 3 },
            new GalleryPhoto { Id = 12, GalleryId= 1, PhotoId = 829, Order = 5 },
            new GalleryPhoto { Id = 13, GalleryId= 1, PhotoId = 70, Order = 4 },
            new GalleryPhoto { Id = 14, GalleryId= 5, PhotoId = 15, Order = 2 },
            new GalleryPhoto { Id = 15, GalleryId= 5, PhotoId = 21, Order = 1 },
            new GalleryPhoto { Id = 16, GalleryId= 3, PhotoId = 29, Order = 3 },
            new GalleryPhoto { Id = 17, GalleryId= 4, PhotoId = 853, Order = 4 },
            new GalleryPhoto { Id = 18, GalleryId= 1, PhotoId = 133, Order = 1 },
            new GalleryPhoto { Id = 19, GalleryId= 2, PhotoId = 81, Order = 2 },
            new GalleryPhoto { Id = 20, GalleryId= 3, PhotoId = 51, Order = 3 },
            new GalleryPhoto { Id = 21, GalleryId= 3, PhotoId = 20, Order = 4 },
            new GalleryPhoto { Id = 22, GalleryId= 3, PhotoId = 134, Order = 6 },
            new GalleryPhoto { Id = 23, GalleryId= 3, PhotoId = 161, Order = 5 }
        };
    }
 
    public static List<Montage> GetMontageDefaultData()
    {
        return new List<Montage>()
        {
            new Montage { Id = 1, Column = 1, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new Montage { Id = 2, Column = 1, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new Montage { Id = 3, Column = 1, Order = 3, Orientation = Helpers.Enums.PhotoOrientation.square },
            new Montage { Id = 4, Column = 1, Order = 4, Orientation = Helpers.Enums.PhotoOrientation.square },
            new Montage { Id = 5, Column = 2, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new Montage { Id = 6, Column = 2, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new Montage { Id = 7, Column = 2, Order = 3, Orientation = Helpers.Enums.PhotoOrientation.square },
            new Montage { Id = 8, Column = 2, Order = 4, Orientation = Helpers.Enums.PhotoOrientation.square },
            new Montage { Id = 9, Column = 2, Order = 5, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new Montage { Id = 10, Column = 2, Order = 6, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new Montage { Id = 11, Column = 3, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.square },
            new Montage { Id = 12, Column = 3, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new Montage { Id = 13, Column = 4, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new Montage { Id = 14, Column = 4, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new Montage { Id = 15, Column = 4, Order = 3, Orientation = Helpers.Enums.PhotoOrientation.square },
            new Montage { Id = 16, Column = 4, Order = 4, Orientation = Helpers.Enums.PhotoOrientation.landscape },
        };
    }

    public static List<Orientation> GetOrientationDefaultData()
    {
        return new List<Orientation>()
        {
            new Orientation { Id = 1, Name = "Landscape" },
            new Orientation { Id = 2, Name = "Portrait" },
            new Orientation { Id = 3, Name = "Square" }
        };
    }

    public static List<Palette> GetPaletteDefaultData()
    {
        return new List<Palette>()
        {
            new Palette { Id = 1, Name = "Colour" },
            new Palette { Id = 2, Name = "Black & White" },
            new Palette { Id = 3, Name = "Infrared" }
        };
    }

    //public static List<Photo> GetPhotoDefaultData()
    //{
    //    return new List<Photo>()
    //    {
    //        CreatePhoto(1, "/Photos/108_2460.jpg", "Juvenile Moray Eel", 2, 4, 16, 2, 3, 1, true, new DateTime(2013, 3, 20)),
    //        CreatePhoto(2, "/Photos/CRW_7734.jpg", "Farmers fixing tractor", 1, 2, 10, 4, 1, 2, true, new DateTime(2006, 1, 16)),
    //        //new Photo { Id = 1, Path = "/Photos/108_2460.jpb", Title = "Juvenile Moray Eel", Camera = new Camera() { Id = 1 } , Category = new Category() { Id = 4}, Country = new Country() { Id = 16 }, DateTaken = new DateTime(2013, 3, 20), Lens = new Lens() { Id = 2 }, Orientation = new Orientation() { Id = 3 }, Palette = new Palette() { Id = 1}, UseInMontage = true },
    //        //new Photo { Id = 2, Name = "Portrait" },
    //        //new Photo { Id = 3, Name = "Square" }
    //    };
    //}

    public static List<Showcase> GetShowcaseDefaultData()
    {
        return new List<Showcase>()
        {
            new Showcase { Id = 1, Name = "Black & White" },
            new Showcase { Id = 2, Name = "Mountains" },
            new Showcase { Id = 3, Name = "Atacama Desert" }
        };
    }

    //private static Photo CreatePhoto(int id, string path, string title, int cameraId, int categoryId, int countryId, int lensId, int orientationId, int paletteId, bool useInMontage, DateTime dateTaken)
    //{
    //    return new Photo()
    //    {
    //        Id = id,
    //        Path = path,
    //        Title = title,
    //        Camera = new Camera() { Id = cameraId },
    //        Category = new Category() { Id = categoryId },
    //        Country = new Country() { Id = countryId },
    //        DateTaken = dateTaken,
    //        Lens = new Lens() { Id = lensId },
    //        Orientation = new Orientation() { Id = orientationId },
    //        Palette = new Palette() { Id = paletteId },
    //        UseInMontage = useInMontage
    //    };
    //}
}
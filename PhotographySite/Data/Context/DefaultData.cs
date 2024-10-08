﻿using PhotographySite.Models;

namespace PhotographySite.Data.Context;

public class DefaultData
{
    public static List<Country> GetCountryDefaultData()
    {
        return
        [
            new() { Id = 1, Name = "England" },
            new() { Id = 2, Name = "Scotland" },
            new() { Id = 3, Name = "Wales" },
            new() { Id = 5, Name = "India" },
            new() { Id = 6, Name = "Australia" },
            new() { Id = 7, Name = "Nepal" },
            new() { Id = 8, Name = "Tibet" },
            new() { Id = 9, Name = "China" },
            new() { Id = 10, Name = "Vietnam" },
            new() { Id = 11, Name = "Cambodia" },
            new() { Id = 12, Name = "Thailand" },
            new() { Id = 13, Name = "Malaysia" },
            new() { Id = 14, Name = "Borneo" },
            new() { Id = 15, Name = "Philippines" },
            new() { Id = 16, Name = "Egypt" },
            new() { Id = 17, Name = "Indonesia" },
            new() { Id = 18, Name = "Peru" },
            new() { Id = 19, Name = "Bolivia" },
            new() { Id = 20, Name = "Chile" },
            new() { Id = 21, Name = "Argentina" },
            new() { Id = 22, Name = "Germany" },
            new() { Id = 23, Name = "Spain" },
            new() { Id = 24, Name = "Myanmar" },
        ];
    }

    public static List<Category> GetCategoryDefaultData()
    {
        return
        [
            new() { Id = 1, Name = "Landscape" },
            new() { Id = 2, Name = "Travel" },
            new() { Id = 3, Name = "Wildlife" },
            new() { Id = 4, Name = "Underwater" },
            new() { Id = 5, Name = "Portrait" },
            new() { Id = 6, Name = "Macro" },
            new() { Id = 7, Name = "Miscellaneous" },
        ];
    }

    public static List<Gallery> GetGalleryDefaultData()
    {
        return
        [
            new() { Id = 1, Name = "Landscape" },
            new() { Id = 2, Name = "Travel" },
            new() { Id = 3, Name = "Wildlife" },
            new() { Id = 4, Name = "Underwater" },
            new() { Id = 5, Name = "Portraits" },
            new() { Id = 6, Name = "Black & White" },
            new() { Id = 7, Name = "Macro" }

        ];
    }

    public static List<GalleryPhoto> GetGalleryPhotoDefaultData()
    {
        return
        [
            new() { Id = 1, GalleryId= 4, PhotoId = 731, Order = 8 },
            new() { Id = 2, GalleryId= 4, PhotoId = 493, Order = 3 },
            new() { Id = 3, GalleryId= 4, PhotoId = 496, Order = 7 },
            new() { Id = 4, GalleryId= 4, PhotoId = 517, Order = 4 },
            new() { Id = 5, GalleryId= 4, PhotoId = 734, Order = 1 },
            new() { Id = 6, GalleryId= 4, PhotoId = 510, Order = 2 },
            new() { Id = 7, GalleryId= 4, PhotoId = 495, Order = 5 },
            new() { Id = 8, GalleryId= 4, PhotoId = 509, Order = 6 },
            new() { Id = 9, GalleryId= 1, PhotoId = 837, Order = 2 },
            new() { Id = 10, GalleryId= 1, PhotoId = 30, Order = 1 },
            new() { Id = 11, GalleryId= 1, PhotoId = 832, Order = 3 },
            new() { Id = 12, GalleryId= 1, PhotoId = 829, Order = 5 },
            new() { Id = 13, GalleryId= 1, PhotoId = 70, Order = 4 },
            new() { Id = 14, GalleryId= 5, PhotoId = 15, Order = 2 },
            new() { Id = 15, GalleryId= 5, PhotoId = 21, Order = 1 },
            new() { Id = 16, GalleryId= 3, PhotoId = 29, Order = 3 },
            new() { Id = 17, GalleryId= 4, PhotoId = 853, Order = 4 },
            new() { Id = 18, GalleryId= 1, PhotoId = 133, Order = 1 },
            new() { Id = 19, GalleryId= 2, PhotoId = 81, Order = 2 },
            new() { Id = 20, GalleryId= 3, PhotoId = 51, Order = 3 },
            new() { Id = 21, GalleryId= 3, PhotoId = 20, Order = 4 },
            new() { Id = 22, GalleryId= 3, PhotoId = 134, Order = 6 },
            new() { Id = 23, GalleryId= 3, PhotoId = 161, Order = 5 }
        ];
    }

    public static List<Montage> GetMontageDefaultData()
    {
        return
        [
            new() { Id = 1, Column = 1, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new() { Id = 2, Column = 1, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new() { Id = 3, Column = 1, Order = 3, Orientation = Helpers.Enums.PhotoOrientation.square },
            new() { Id = 4, Column = 1, Order = 4, Orientation = Helpers.Enums.PhotoOrientation.square },
            new() { Id = 5, Column = 2, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new() { Id = 6, Column = 2, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new() { Id = 7, Column = 2, Order = 3, Orientation = Helpers.Enums.PhotoOrientation.square },
            new() { Id = 8, Column = 2, Order = 4, Orientation = Helpers.Enums.PhotoOrientation.square },
            new() { Id = 9, Column = 2, Order = 5, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new() { Id = 10, Column = 2, Order = 6, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new() { Id = 11, Column = 3, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.square },
            new() { Id = 12, Column = 3, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.landscape },
            new() { Id = 13, Column = 4, Order = 1, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new() { Id = 14, Column = 4, Order = 2, Orientation = Helpers.Enums.PhotoOrientation.portrait },
            new() { Id = 15, Column = 4, Order = 3, Orientation = Helpers.Enums.PhotoOrientation.square },
            new() { Id = 16, Column = 4, Order = 4, Orientation = Helpers.Enums.PhotoOrientation.landscape },
        ];
    }

    public static List<Orientation> GetOrientationDefaultData()
    {
        return
        [
            new() { Id = 1, Name = "Landscape" },
            new() { Id = 2, Name = "Portrait" },
            new() { Id = 3, Name = "Square" }
        ];
    }

    public static List<Palette> GetPaletteDefaultData()
    {
        return
        [
            new() { Id = 1, Name = "Colour" },
            new() { Id = 2, Name = "Black & White" },
            new() { Id = 3, Name = "Infrared" }
        ];
    }
}
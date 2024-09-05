using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Dto.Response;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Dto.Request;
using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Helpers;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<GalleryAddRequest, Gallery>().ReverseMap();
        CreateMap<GalleryUpdateRequest, Gallery>().ReverseMap();
        CreateMap<GalleryActionResponse, Gallery>().ReverseMap();
        CreateMap<GalleryResponse, Gallery>().ReverseMap();
        CreateMap<PhotoResponse, Photo>().ReverseMap();
        CreateMap<GalleryPhotoAddRequest, GalleryPhoto>().ReverseMap();

        base.CreateMap<GalleryPhoto, GalleryPhotoResponse>()
                .ForMember(dest => dest.PhotoId, act => act.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.FileName, act => act.MapFrom(src => src.Photo.FileName))
                .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Photo.Country.Name))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Photo.Title))
                .ForMember(dest => dest.IsFavourite, act => act.MapFrom(src => src.Photo.Favourites.Count > 0)).ReverseMap();

        base.CreateMap<UserGalleryPhoto, UserGalleryPhotoResponse>()
                .ForMember(dest => dest.PhotoId, act => act.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.FileName, act => act.MapFrom(src => src.Photo.FileName))
                .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Photo.Country.Name))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Photo.Title))
                .ForMember(dest => dest.IsFavourite, act => act.MapFrom(src => src.Photo.Favourites.Count > 0)).ReverseMap();

        CreateMap<UserGalleryAddRequest, UserGallery>().ReverseMap();
        CreateMap<UserGalleryUpdateRequest, UserGallery>().ReverseMap();
        CreateMap<UserGalleryResponse, UserGallery>().ReverseMap();
        CreateMap<Montage, MontageResponse>().ReverseMap();
        CreateMap<UserGalleryPhoto, UserGalleryPhotoRequest>().ReverseMap();
        CreateMap<Favourite, FavouriteAddRequest>().ReverseMap();

        CreateMap<Photo, FavouriteResponse>()
                .ForMember(dest => dest.PhotoId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.FileName, act => act.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.Orientation, act => act.MapFrom(src => src.Orientation))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title));

        CreateMap<CountryResponse, Country>();
        CreateMap<Country, CountryResponse>();
        CreateMap<CategoryResponse, Category>();
        CreateMap<Category, CategoryResponse>();
        CreateMap<PaletteResponse, Palette>();
        CreateMap<Palette, PaletteResponse>();
    }
}
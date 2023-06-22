﻿using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Helpers;

public class AutoMapperProfile : AutoMapper.Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Montage, MontageDto>();
        CreateMap<MontageDto, Montage>();
        CreateMap<CountryDto, Country>();
        CreateMap<Country, CountryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
        CreateMap<PaletteDto, Palette>();
        CreateMap<Palette, PaletteDto>();
        CreateMap<PhotoDto, Photo>();
        CreateMap<Photo, PhotoDto>();
        CreateMap<GalleryDto, Gallery>();
        CreateMap<Gallery, GalleryDto>();
        CreateMap<GalleryNameDto, Gallery>();
        CreateMap<Gallery, GalleryNameDto>();
        CreateMap<GalleryPhotoDto, GalleryPhoto>(); 
        CreateMap<UserGalleryDto, UserGallery>();
        CreateMap<UserGallery, UserGalleryDto>();
        CreateMap<UserGalleryNameDto, UserGallery>();
        CreateMap<UserGallery, UserGalleryNameDto>();
        CreateMap<UserGalleryPhotoDto, UserGalleryPhoto>();
        CreateMap<UserGalleryPhoto, UserGalleryPhotoDto>();

        base.CreateMap<GalleryPhoto, GalleryPhotoDto>()
                .ForMember(dest => dest.PhotoId, act => act.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.FileName, act => act.MapFrom(src => src.Photo.FileName))
                .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Photo.Country.Name))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Photo.Title))
                .ForMember(dest => dest.IsFavourite, act => act.MapFrom(src => src.Photo.Favourites.Count > 0 ? true : false));

		base.CreateMap<UserGalleryPhoto, UserGalleryPhotoDto>()
				.ForMember(dest => dest.PhotoId, act => act.MapFrom(src => src.Id))
				.ForMember(dest => dest.FileName, act => act.MapFrom(src => src.Photo.FileName))
				.ForMember(dest => dest.Country, act => act.MapFrom(src => src.Photo.Country.Name))
				.ForMember(dest => dest.Title, act => act.MapFrom(src => src.Photo.Title))
                .ForMember(dest => dest.IsFavourite, act => act.MapFrom(src => src.Photo.Favourites.Count > 0 ? true : false));

        base.CreateMap<Photo, FavouriteDto>()
                .ForMember(dest => dest.PhotoId, act => act.MapFrom(src => src.Id))
                .ForMember(dest => dest.FileName, act => act.MapFrom(src => src.FileName))
                .ForMember(dest => dest.Country, act => act.MapFrom(src => src.Country.Name))
                .ForMember(dest => dest.Orientation, act => act.MapFrom(src => src.Orientation))
                .ForMember(dest => dest.Title, act => act.MapFrom(src => src.Title));
    }
}
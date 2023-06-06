using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Helpers;

public class MontageHelper
{
    const string templatePath = "/images/"; 
    const string portraitTemplate = "PortraitTemplate.jpg";
    const string squareTemplate = "SquareTemplate.jpg";
    const string landscapeTemplate = "LandscapeTemplate.jpg";

    public static List<MontageDto> AddMontageTemplateImages(List<MontageDto> montageImagesColumn)
    {
        foreach (MontageDto montageDto in montageImagesColumn)
        {                
            if(IsPortrait(montageDto.Orientation))
            {
                montageDto.Path = templatePath + portraitTemplate;
            } 
            else if (IsSquare(montageDto.Orientation))
            {
                montageDto.Path = templatePath + squareTemplate;
            } 
            else if (IsLandscape(montageDto.Orientation))
            {
                montageDto.Path = templatePath + landscapeTemplate;
            }
        };

        return montageImagesColumn;
    }

    public static List<MontageDto> AddMontagePhotos(List<MontageDto> montageImagesColumn, List<Photo> squarePhotos, List<Photo> portraitPhotos, List<Photo> landscapePhotos)
    {
        foreach (MontageDto montageDto in montageImagesColumn)
        {
            if (IsPortrait(montageDto.Orientation))
            { 
                if(portraitPhotos.Count > 0) {
                    UpdateMontageDto(montageDto, portraitPhotos.First());
                    portraitPhotos.RemoveAt(0);
                }
            }
            else if (IsSquare(montageDto.Orientation))
            {
                if (squarePhotos.Count > 0)
                {
                    UpdateMontageDto(montageDto, squarePhotos.First());
                    squarePhotos.RemoveAt(0);
                }
            }
            else if (IsLandscape(montageDto.Orientation))
            {
                if (landscapePhotos.Count > 0)
                {
                    UpdateMontageDto(montageDto, landscapePhotos.First()); 
                    landscapePhotos.RemoveAt(0);
                }
            }
        };

        return montageImagesColumn;
    }

    private static void UpdateMontageDto(MontageDto montageDto, Photo photo)
    {
        montageDto.Path = photo.FileName;
		montageDto.PhotoId = photo.Id;
        montageDto.IsFavourite = photo.Favourites != null & photo.Favourites.Count() > 0 ? true : false;
        montageDto.Title = photo.Title + (photo.Country != null ? " - " + photo.Country.Name : "");
    }

    private static bool IsPortrait(int orientation)
    {
        return (Helpers.Enums.PhotoOrientation)orientation == Helpers.Enums.PhotoOrientation.portrait;
    }

    private static bool IsLandscape(int orientation)
    {
        return (Helpers.Enums.PhotoOrientation)orientation == Helpers.Enums.PhotoOrientation.landscape;
    }

    private static bool IsSquare(int orientation)
    {
        return (Helpers.Enums.PhotoOrientation)orientation == Helpers.Enums.PhotoOrientation.square;
    }
}    
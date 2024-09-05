using PhotographySite.Dto.Response;
using PhotographySite.Models;

namespace PhotographySite.Helpers;

public class MontageHelper
{
    public static List<MontageResponse> AddMontageTemplateImages(List<MontageResponse> montageImagesColumn)
    {
        foreach (MontageResponse montageResponse in montageImagesColumn)
        {
            if (IsPortrait(montageResponse.Orientation))
                montageResponse.Path = Constants.TemplatePath + Constants.PortraitTemplate;
            else if (IsSquare(montageResponse.Orientation))
                montageResponse.Path = Constants.TemplatePath + Constants.SquareTemplate;
            else if (IsLandscape(montageResponse.Orientation))
                montageResponse.Path = Constants.TemplatePath + Constants.LandscapeTemplate;
        };

        return montageImagesColumn;
    }

    public static List<MontageResponse> AddMontagePhotos(List<MontageResponse> montageImagesColumn, List<Photo> squarePhotos, List<Photo> portraitPhotos, List<Photo> landscapePhotos)
    {
        foreach (MontageResponse montageResponse in montageImagesColumn)
        {
            if (IsPortrait(montageResponse.Orientation))
                portraitPhotos = UpdateMontageList(portraitPhotos, montageResponse);
            else if (IsSquare(montageResponse.Orientation))
                squarePhotos = UpdateMontageList(squarePhotos, montageResponse);
            else if (IsLandscape(montageResponse.Orientation))
                landscapePhotos = UpdateMontageList(landscapePhotos, montageResponse);
        };

        return montageImagesColumn;
    }

    private static List<Photo> UpdateMontageList(List<Photo> photos, MontageResponse montageResponse)
    {
        if (photos.Count > 0)
        {
            UpdateMontageRequest(montageResponse, photos.First());
            photos.RemoveAt(0);
        }

        return photos;
    }

    private static void UpdateMontageRequest(MontageResponse montageResponse, Photo photo)
    {
        montageResponse.Path = photo.FileName;
        montageResponse.PhotoId = photo.Id;
        montageResponse.IsFavourite = photo.Favourites != null & photo.Favourites.Count > 0;
        montageResponse.Title = photo.Title + (photo.Country != null ? " - " + photo.Country.Name : "");
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
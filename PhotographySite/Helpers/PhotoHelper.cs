using static PhotographySite.Helpers.Enums;

namespace PhotographySite.Helpers;

public class PhotoHelper
{
    public static PhotoOrientation GetOrientation(int width, int height)
    {
        if (width == height)
            return PhotoOrientation.square;
        else if (width > height)
            return PhotoOrientation.landscape;
        else
            return PhotoOrientation.portrait;
    }
}
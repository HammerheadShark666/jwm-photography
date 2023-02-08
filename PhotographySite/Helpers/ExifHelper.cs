using MetadataExtractor;
using PhotographySite.Models;

namespace PhotographySite.Helpers;

public class ExifHelper
{
    public static Photo GetExifData(string path)
    {
        Photo photo = new Photo();
        IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(path);

        Parallel.ForEach(directories, directory =>
        {
            foreach (var tag in directory.Tags)
            {
                switch (directory.Name)
                {
                    case "Exif IFD0":
                        {                            
                            photo = GetExifIFD0(photo, tag);
                            break;
                        }
                    case "Exif SubIFD":
                        {                            
                            photo = GetExifSubIFD(photo, tag);
                            break;
                        }
                    case "JPEG":
                        {   
                            photo = GetJPEG(photo, tag);   
                            break;
                        }
                    case "File":
                        {    
                            photo = GetFile(photo, tag);
                            break;
                        }
                }
            }
        });

        return photo;
    }

    private static Photo GetFile(Photo photo , Tag tag)
    {
        switch (tag.Name)
        {
            case "File Name":
                {
                    photo.FileName = tag.Description;
                    break;
                }
        }

        return photo;
    }

    private static Photo GetJPEG(Photo photo, Tag tag)
    {
        switch (tag.Name)
        {
            case "Image Height":
                {
                    photo.Height = int.Parse(tag.Description.Replace(" pixels", ""));
                    break;
                }
            case "Image Width":
                {
                    photo.Width = int.Parse(tag.Description.Replace(" pixels", ""));
                    break;
                }
        }

        return photo;
    }

    private static Photo GetExifIFD0(Photo photo, Tag tag)
    {
        switch (tag.Name)
        {
            case "Model":
                {
                    photo.Camera = tag.Description;
                    break;
                }
        }

        return photo;
    }

    private static Photo GetExifSubIFD(Photo photo, Tag tag)
    {
        switch (tag.Name)
        {
            case "Exposure Time":
                {
                    photo.ExposureTime = tag.Description;
                    break;
                }
            case "F-Number":
            case "Aperture Value":
                {
                    photo.AperturValue = tag.Description;
                    break;
                }
            case "Exposure Program":
                {
                    photo.ExposureProgram = tag.Description;
                    break;
                }
            case "ISO Speed Ratings":
                {
                    if(int.Parse(tag.Description) > 0)
                        photo.Iso = int.Parse(tag.Description);
                    break;
                }
            case "Date/Time Original":
                {
                    try
                    {
                        photo.DateTaken = DateTime.ParseExact(tag.Description, "yyyy:MM:dd HH:mm:ss", null);
                    }
                    catch (Exception ex) { }
                    break;
                }
            case "Focal Length":
                {
                    photo.FocalLength = tag.Description;
                    break;
                }
            case "Lens Specification":
            case "Lens Model":
                {
                    photo.Lens = tag.Description;
                    break;
                }
        }

        return photo;
    }
}
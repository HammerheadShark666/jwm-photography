namespace PhotographySite.Models.Dto;

public class UserGalleryPhotoDto
{
    public long UserGalleryId { get; set; }

    public long PhotoId { get; set; }

    public int Order { get; set; }

    public string Title { get; set; }

    public string FileName { get; set; }

    public string Country { get; set; }

    public bool IsFavourite { get; set; }
}
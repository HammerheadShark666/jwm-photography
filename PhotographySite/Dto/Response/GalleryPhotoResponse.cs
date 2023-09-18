namespace PhotographySite.Dto.Response;

public class GalleryPhotoResponse
{
    public long GalleryId { get; set; }
    public long PhotoId { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string Country { get; set; }
    public bool IsFavourite { get; set; }
}
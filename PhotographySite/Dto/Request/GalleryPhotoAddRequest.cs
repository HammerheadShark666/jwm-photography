namespace PhotographySite.Dto.Request;

public class GalleryPhotoAddRequest
{
    public long GalleryId { get; set; }
    public long PhotoId { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string Country { get; set; }
    public bool IsGallery { get; set; }
}
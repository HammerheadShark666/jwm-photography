namespace PhotographySite.Areas.Site.Dto.Response;

public class UserGalleryPhotoResponse
{
    public long FavouriteId { get; set; }
    public long PhotoId { get; set; }
    public int Order { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string Country { get; set; }
    public bool IsFavourite { get; set; }
}
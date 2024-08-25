namespace PhotographySite.Areas.Site.Dto.Request;

public class UserGalleryRemoveRequest
{
    public Guid UserId { get; set; }
    public long UserGalleryId { get; set; }
    public long PhotoId { get; set; }
}
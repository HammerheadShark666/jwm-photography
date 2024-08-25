namespace PhotographySite.Areas.Site.Dto.Request;

public class UserGalleryAddRequest
{
    public string Name { get; set; }
    public Guid UserId { get; set; }

    public UserGalleryAddRequest() { }
}
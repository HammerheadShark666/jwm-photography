namespace PhotographySite.Areas.Site.Dto.Request;

public class UserGalleryUpdateRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }

    public UserGalleryUpdateRequest() { }
}
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Dto.Response;

namespace PhotographySite.Dto.Request;

public class MenuBarRequest
{
    public GalleriesResponse Galleries { get; set; }
    public List<UserGalleryResponse> UserGalleries { get; set; }
}
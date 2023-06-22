using PhotographySite.Areas.Admin.Dtos;

namespace PhotographySite.Models.Dto;

public class MenuBarDto
{
	public GalleriesDto Galleries { get; set; }

	public List<UserGalleryDto> UserGalleries { get; set; }
}

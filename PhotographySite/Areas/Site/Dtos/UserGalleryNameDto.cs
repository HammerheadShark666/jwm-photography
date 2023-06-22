using PhotographySite.Models.Dto;

namespace PhotographySite.Models.Dto;

public class UserGalleryNameDto : BaseDto
{
    public long Id { get; set; }

	public Guid UserId { get; set; }

	public string Name { get; set; }     
}
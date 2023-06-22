namespace PhotographySite.Models.Dto;

public class UserGalleryDto : BaseDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string RandomPhoto { get; set; }
    
    public List<UserGalleryPhotoDto> Photos { get; set; }

	public string AzureStoragePath { get; set; }
}
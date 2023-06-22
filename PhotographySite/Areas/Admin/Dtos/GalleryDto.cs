using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Dtos;

public class GalleryDto : BaseDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public string RandomPhoto { get; set; }
    
    public List<GalleryPhotoDto> Photos { get; set; }

	public string AzureStoragePath { get; set; } 
}
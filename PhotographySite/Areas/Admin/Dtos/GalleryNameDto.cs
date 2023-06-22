using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Dtos;

public class GalleryNameDto : BaseDto
{
    public long Id { get; set; }

    public string Name { get; set; }     
}
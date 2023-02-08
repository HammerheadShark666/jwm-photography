using PhotographySite.Models;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Models;

public class GalleryNameDto : BaseDto
{
    public long Id { get; set; }

    public string Name { get; set; }     
}
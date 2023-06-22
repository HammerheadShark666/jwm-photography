using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Dtos;

public class LookupsDto
{
    public List<CountryDto> Countries { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<PaletteDto> Palettes { get; set; }
    public List<CameraDto> Cameras { get; set; }
}

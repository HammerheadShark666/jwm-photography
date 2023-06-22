using PhotographySite.Areas.Admin.Dtos;

namespace PhotographySite.Models.Dto;

public class LookupsPhotoCatalogueDto
{
	public List<CountryDto> Countries { get; set; }
	public List<CategoryDto> Categories { get; set; }
	public List<PaletteDto> Palettes { get; set; }
}

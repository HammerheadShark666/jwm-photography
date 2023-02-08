using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IPaletteService
{
	Task<List<PaletteDto>> GetPalettesAsync();
}

using PhotographySite.Areas.Site.Dto.Response;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface IPaletteService
{
    Task<List<PaletteResponse>> GetPalettesAsync();
}
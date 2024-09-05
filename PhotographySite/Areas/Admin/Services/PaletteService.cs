using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Admin.Services;

public class PaletteService(IUnitOfWork unitOfWork, IMapper mapper) : IPaletteService
{
    public async Task<List<PaletteResponse>> GetPalettesAsync()
    {
        return mapper.Map<List<PaletteResponse>>(await unitOfWork.Palettes.AllSortedAsync());
    }
}
using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Admin.Services;

public class PaletteService : IPaletteService
{
	private IUnitOfWork _unitOfWork;
	private IMapper _mapper;

	public PaletteService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<List<PaletteResponse>> GetPalettesAsync()
	{
		return _mapper.Map<List<PaletteResponse>>(await _unitOfWork.Palettes.AllSortedAsync());
	}
}
using AutoMapper;
using FluentValidation;
using PhotographySite.Areas.Admin.Business;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;
using PhotographySite.Models.Dto;

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

	public async Task<List<PaletteDto>> GetPalettesAsync()
	{
		return _mapper.Map<List<PaletteDto>>(await _unitOfWork.Palettes.AllSortedAsync());
	}
}

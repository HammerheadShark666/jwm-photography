using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services;

public class CountryService : ICountryService
{
	private IUnitOfWork _unitOfWork;
	private IMapper _mapper;

	public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<List<CountryDto>> GetCountriesAsync()
	{
		return _mapper.Map<List<CountryDto>>(await _unitOfWork.Countries.AllSortedAsync());
	}
}

using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;

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

	public async Task<List<CountryResponse>> GetCountriesAsync()
	{
		return _mapper.Map<List<CountryResponse>>(await _unitOfWork.Countries.AllSortedAsync());
	}
}
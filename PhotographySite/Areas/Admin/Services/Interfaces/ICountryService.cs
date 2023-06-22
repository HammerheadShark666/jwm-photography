using PhotographySite.Areas.Admin.Dtos;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface ICountryService
{
	Task<List<CountryDto>> GetCountriesAsync();
}

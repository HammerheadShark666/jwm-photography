using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface ICountryService
{
	Task<List<CountryDto>> GetCountriesAsync();
}

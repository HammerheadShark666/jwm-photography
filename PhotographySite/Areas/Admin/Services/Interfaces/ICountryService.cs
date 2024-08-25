using PhotographySite.Areas.Site.Dto.Response;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface ICountryService
{
    Task<List<CountryResponse>> GetCountriesAsync();
}
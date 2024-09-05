using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Admin.Services;

public class CountryService(IUnitOfWork unitOfWork, IMapper mapper) : ICountryService
{
    public async Task<List<CountryResponse>> GetCountriesAsync()
    {
        return mapper.Map<List<CountryResponse>>(await unitOfWork.Countries.AllSortedAsync());
    }
}
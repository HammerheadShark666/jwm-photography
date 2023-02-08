using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface ICountryRepository : IBaseRepository<Country>
{
    Task<List<Country>> AllSortedAsync();
}

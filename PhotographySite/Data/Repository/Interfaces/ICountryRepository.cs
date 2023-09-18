using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface ICountryRepository
{
    Task<List<Country>> AllSortedAsync();
    Task<Country> ByIdAsync(int id);
}
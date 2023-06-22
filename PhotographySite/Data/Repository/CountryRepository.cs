using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class CountryRepository : BaseRepository<Country>, ICountryRepository
{
    public CountryRepository(PhotographySiteDbContext context) : base(context) { }

    public async Task<List<Country>> AllSortedAsync()
    {
        return await _context.Country.OrderBy(country => country.Name).ToListAsync();
    }
}
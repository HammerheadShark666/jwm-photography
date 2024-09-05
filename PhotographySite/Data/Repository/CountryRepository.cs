using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Context;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class CountryRepository(PhotographySiteDbContext context) : ICountryRepository
{
    public async Task<List<Country>> AllSortedAsync()
    {
        return await context.Country.OrderBy(country => country.Name).ToListAsync();
    }

    public async Task<Country> ByIdAsync(int id)
    {
        return await context.Country.FindAsync(id);
    }
}
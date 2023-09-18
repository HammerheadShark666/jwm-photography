using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class CountryRepository : ICountryRepository
{
    private readonly PhotographySiteDbContext _context;

    public CountryRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public async Task<List<Country>> AllSortedAsync()
    {
        return await _context.Country.OrderBy(country => country.Name).ToListAsync();
    }

    public async Task<Country> ByIdAsync(int id)
    {
        return await _context.Country.FindAsync(id);
    }
}
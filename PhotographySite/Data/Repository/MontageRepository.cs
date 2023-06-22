using Microsoft.EntityFrameworkCore;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class MontageRepository : BaseRepository<Montage>, IMontageRepository
{
    public MontageRepository(PhotographySiteDbContext context) : base(context) { }

    public async Task<List<Montage>> AllSortedAsync()
    {
        return await _context.Montage.OrderBy(montage => montage.Column).ThenBy(montage => montage.Order).ToListAsync();
    }
}
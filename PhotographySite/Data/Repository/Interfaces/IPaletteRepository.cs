using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IPaletteRepository : IBaseRepository<Palette>
{
    Task<List<Palette>> AllSortedAsync();
}

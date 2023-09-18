using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IPaletteRepository
{
    Task<List<Palette>> AllSortedAsync();
    Task<Palette> ByIdAsync(int id);
}
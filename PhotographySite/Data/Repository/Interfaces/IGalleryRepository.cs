using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IGalleryRepository : IBaseRepository<Gallery>
{
    Task<List<Gallery>> AllSortedAsync();

    Task<bool> ExistsAsync(string name);

    Task<bool> ExistsAsync(long id, string name);

    Task<Gallery> GetFullGalleryAsync(long id);
}

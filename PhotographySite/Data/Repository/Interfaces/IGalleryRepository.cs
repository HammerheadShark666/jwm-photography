using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IGalleryRepository
{
    Task<List<Gallery>> AllSortedAsync();
    Task<bool> ExistsAsync(string name);
    Task<bool> ExistsAsync(long id, string name);
    Task<Gallery> GetFullGalleryAsync(long id);
    Task<Gallery> GetFullGalleryAsync(Guid userId, long id);
    Task AddAsync(Gallery gallery);
    void Update(Gallery gallery);
    void Delete(Gallery gallery);
    Task<Gallery> ByIdAsync(long id);
}
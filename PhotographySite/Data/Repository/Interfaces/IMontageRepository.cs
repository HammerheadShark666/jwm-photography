using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IMontageRepository : IBaseRepository<Montage>
{
    Task<List<Montage>> AllSortedAsync();
}
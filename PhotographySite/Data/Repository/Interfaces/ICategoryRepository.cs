using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<List<Category>> AllSortedAsync();
}

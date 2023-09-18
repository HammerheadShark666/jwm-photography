using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> AllSortedAsync();
    Task<Category> ByIdAsync(int id);
}
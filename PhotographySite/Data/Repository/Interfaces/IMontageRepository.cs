using PhotographySite.Models;
using System.Linq.Expressions;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IMontageRepository
{
    Task<IEnumerable<Montage>> FindAsync(Expression<Func<Montage, bool>> expression);
    Task<List<Montage>> AllSortedAsync();
    Task<Montage> ByIdAsync(int id);
    Task<Montage> AddAsync(Montage montage); 
    void Update(Montage montage);
    void Delete(Montage montage);     
}
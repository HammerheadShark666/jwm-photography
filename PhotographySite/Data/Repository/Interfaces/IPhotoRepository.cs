using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IPhotoRepository
{
    Task<List<Photo>> AllAsync();
    Task<int> CountAsync();
    List<Photo> MontagePhotos(Helpers.Enums.PhotoOrientation orientation, int numberOfPhotos, Guid userId);     
    Task<List<Photo>> ByPagingAsync(PhotoFilterRequest photoFilterRequest);     
    Task<int> ByFilterCountAsync(PhotoFilterRequest photoFilterRequest);
    bool Exists(string Filename);
    Task<Photo> FindByFilenameAsync(string filename);
    Task<List<Photo>> GetLatestPhotos(int numberOfPhotos);
    Task AddAsync(Photo photo);
    void Update(Photo photo);
    Task<Photo> ByIdAsync(long id); 
}
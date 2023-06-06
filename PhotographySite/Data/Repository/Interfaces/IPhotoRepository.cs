using PhotographySite.Areas.Admin.Models;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository.Interfaces;

public interface IPhotoRepository : IBaseRepository<Photo>
{
    new Task<List<Photo>> AllAsync();

    Task<int> CountAsync();

    List<Photo> MontagePhotos(Helpers.Enums.PhotoOrientation orientation, int numberOfPhotos, Guid userId);
     
    Task<List<Photo>> ByPagingAsync(PhotoFilterDto photoFilterDto);
     
    Task<int> ByFilterCountAsync(PhotoFilterDto photoFilterDto);

    bool Exists(string Filename);

    Task<Photo> FindByFilenameAsync(string filename);
}

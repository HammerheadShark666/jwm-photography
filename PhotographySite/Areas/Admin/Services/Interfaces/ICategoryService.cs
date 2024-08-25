using PhotographySite.Areas.Site.Dto.Response;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface ICategoryService
{
    Task<List<CategoryResponse>> GetCategoriesAsync();
}
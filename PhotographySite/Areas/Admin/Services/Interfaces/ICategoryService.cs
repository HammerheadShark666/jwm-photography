using PhotographySite.Areas.Admin.Dtos;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface ICategoryService
{
	Task<List<CategoryDto>> GetCategoriesAsync();
}

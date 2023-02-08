using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Admin.Services.Interfaces;

public interface ICategoryService
{
	Task<List<CategoryDto>> GetCategoriesAsync();
}

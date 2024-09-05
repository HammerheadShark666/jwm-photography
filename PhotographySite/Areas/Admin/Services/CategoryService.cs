using AutoMapper;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Admin.Services;

public class CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : ICategoryService
{
    public async Task<List<CategoryResponse>> GetCategoriesAsync()
    {
        return mapper.Map<List<CategoryResponse>>(await unitOfWork.Categories.AllSortedAsync());
    }
}
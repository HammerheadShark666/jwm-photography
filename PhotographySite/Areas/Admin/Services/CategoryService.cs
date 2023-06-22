using AutoMapper;
using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Admin.Services;

public class CategoryService : ICategoryService
{
	private IUnitOfWork _unitOfWork;
	private IMapper _mapper;

	public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<List<CategoryDto>> GetCategoriesAsync()
	{
		return _mapper.Map<List<CategoryDto>>(await _unitOfWork.Categories.AllSortedAsync());
	}
}

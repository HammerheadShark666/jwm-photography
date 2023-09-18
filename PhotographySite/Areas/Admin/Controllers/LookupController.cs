using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/lookup")]
public class LookupController : Controller
{
	private ICountryService _countryService;
	private ICategoryService _categoryService;
	private IPaletteService _paletteService;

	public LookupController(ICountryService countryService, ICategoryService categoryService, IPaletteService paletteService)
    {
        _countryService = countryService;
		_categoryService = categoryService;
		_paletteService = paletteService;
    }

	[HttpGet("photo-catalogue")]
	public async Task<IActionResult> LookupsForPhotoCatalogueAsync()
	{ 
        return Ok(new LookupsResponse()
		{
			Countries = await _countryService.GetCountriesAsync(),
			Categories = await _categoryService.GetCategoriesAsync(),
			Palettes = await _paletteService.GetPalettesAsync()
		});		
	}
}
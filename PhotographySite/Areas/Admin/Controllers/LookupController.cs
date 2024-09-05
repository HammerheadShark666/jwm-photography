using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/lookup")]
public class LookupController(ICountryService countryService, ICategoryService categoryService, IPaletteService paletteService) : Controller
{
    [HttpGet("photo-catalogue")]
    public async Task<IActionResult> LookupsForPhotoCatalogueAsync()
    {
        return Ok(new LookupsResponse()
        {
            Countries = await countryService.GetCountriesAsync(),
            Categories = await categoryService.GetCategoriesAsync(),
            Palettes = await paletteService.GetPalettesAsync()
        });
    }
}
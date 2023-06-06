using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Models;
using PhotographySite.Areas.Admin.Services.Interfaces;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/photo/catalog")]
public class PhotoCatalogController : Controller
{   
    private IPhotoCatalogService _photoCatalogService;

    public PhotoCatalogController(IPhotoCatalogService photoCatalogService)
    {
        _photoCatalogService = photoCatalogService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Catalog()
    {
        return View("PhotoCatalogue", await _photoCatalogService.GetLookupsAsync());
    }

    [HttpPost("")]
    public async Task<IActionResult> PhotoCatalogAsync([FromBody] PhotoFilterDto photoFilterDto)
    {
        return new JsonResult(await _photoCatalogService.GetPhotosPageAsync(photoFilterDto));
    }

    [HttpPost("update-photo")]
    public async Task<IActionResult> AddUpdatePhotoAsync([FromBody] UpdatePhotoDto updatePhotoDto)
    {
        await _photoCatalogService.UpdatePhotoAsync(updatePhotoDto);
        return Ok();
    }
}

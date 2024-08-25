using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Services.Interfaces;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/photo/catalog")]
[AutoValidateAntiforgeryToken]
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
    public async Task<IActionResult> PhotoCatalogAsync([FromBody] PhotoFilterRequest photoFilterRequest)
    {
        return Ok(await _photoCatalogService.GetPhotosPageAsync(photoFilterRequest));
    }

    [HttpPost("update-photo")]
    public async Task<IActionResult> AddUpdatePhotoAsync([FromBody] UpdatePhotoRequest updatePhotoRequest)
    {
        await _photoCatalogService.UpdatePhotoAsync(updatePhotoRequest);
        return Ok();
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Dto.Request;
using PhotographySite.Areas.Admin.Services.Interfaces;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/photo/catalog")]
[AutoValidateAntiforgeryToken]
public class PhotoCatalogController(IPhotoCatalogService photoCatalogService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Catalog()
    {
        return View("PhotoCatalogue", await photoCatalogService.GetLookupsAsync());
    }

    [HttpPost("")]
    public async Task<IActionResult> PhotoCatalogAsync([FromBody] PhotoFilterRequest photoFilterRequest)
    {
        return Ok(await photoCatalogService.GetPhotosPageAsync(photoFilterRequest));
    }

    [HttpPost("update-photo")]
    public async Task<IActionResult> AddUpdatePhotoAsync([FromBody] UpdatePhotoRequest updatePhotoRequest)
    {
        await photoCatalogService.UpdatePhotoAsync(updatePhotoRequest);
        return Ok();
    }
}
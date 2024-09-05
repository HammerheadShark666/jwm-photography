using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin")]
public class HomeController(IPhotoCatalogService photoCatalogService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View("Home", new GalleriesResponse()
        {
            SelectGalleryPhotos = await photoCatalogService.GetLatestPhotos(20)
        });
    }
}
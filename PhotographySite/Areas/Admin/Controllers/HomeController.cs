using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private IPhotoCatalogService _photoCatalogService;

    public HomeController(ILogger<HomeController> logger, IPhotoCatalogService photoCatalogService)
    {
        _logger = logger;
        _photoCatalogService = photoCatalogService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        return View("Home", new GalleriesResponse()
        {
            SelectGalleryPhotos = await _photoCatalogService.GetLatestPhotos(20)
        });
    }
}
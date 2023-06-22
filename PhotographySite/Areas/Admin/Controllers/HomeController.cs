using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Areas.Admin.Services.Interfaces;

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
        GalleriesDto galleriesDto = new()
        {
            SelectGalleryPhotos = await _photoCatalogService.GetLatestPhotos(20)
        };

        return View("Home", galleriesDto); 
    }

    public IActionResult Privacy()
    {
        return View();
    } 
}
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("")]
public class HomeController : BaseController
{
    private IMontageService _montageService;

    public HomeController(IMontageService montageService, IUserService userService) : base(userService)
    {
        _montageService = montageService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        Guid userId = Guid.Empty;

        if (IsLoggedIn())
            userId = GetUserId();

        return View(await _montageService.GetMontageAsync(userId));
    }
}
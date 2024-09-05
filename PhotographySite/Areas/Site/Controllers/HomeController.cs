using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("")]
public class HomeController(IMontageService montageService, IUserService userService) : BaseController(userService)
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        Guid userId = Guid.Empty;

        if (IsLoggedIn())
            userId = GetUserId();

        return View(await montageService.GetMontageAsync(userId));
    }
}
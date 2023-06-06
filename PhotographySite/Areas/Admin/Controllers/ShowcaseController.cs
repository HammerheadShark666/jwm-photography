using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PhotographySite.Areas.Admin.Controllers;

[Authorize(Roles = "Admin")]
[Area("Admin")]
[Route("admin/showcase")]
public class ShowcaseController : Controller
{
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
}

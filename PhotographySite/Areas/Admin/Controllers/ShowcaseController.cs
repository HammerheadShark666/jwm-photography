using Microsoft.AspNetCore.Mvc;

namespace PhotographySite.Areas.Admin.Controllers;

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

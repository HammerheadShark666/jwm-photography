using Microsoft.AspNetCore.Mvc;
using PhotographySite.Models;
using System.Diagnostics;

namespace PhotographySite.Controllers;

[Route("error")]
public class ErrorController : Controller
{
    [HttpGet("")]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

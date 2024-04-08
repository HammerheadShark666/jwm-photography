using Microsoft.AspNetCore.Mvc;
using PhotographySite.Models;
using System.Diagnostics;

namespace PhotographySite.Controllers;

[Route("error")]
public class ErrorController : Controller
{
    [HttpGet("{code:int}")]
    public IActionResult Http(int code)
    {
        switch (code)
        {
            case 401:
                {
                    return View("Error401"); 
                }
            case 404:
                {
                    return View("Error404");
                }
            default:
                return View();
        }  
    }

    [HttpGet("")]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
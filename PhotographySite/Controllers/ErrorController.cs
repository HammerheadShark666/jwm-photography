﻿using Microsoft.AspNetCore.Mvc;
using PhotographySite.Models;
using System.Diagnostics;

namespace PhotographySite.Controllers;

[Route("error")]
public class ErrorController : Controller
{
    [HttpGet("http/{code:int}")]
    public IActionResult Http(int code)
    { 
        if (code == 401)
            return View("Error401");

        if (code == 404)
            return View("Error404");

        return View();
    }

    [HttpGet("")]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
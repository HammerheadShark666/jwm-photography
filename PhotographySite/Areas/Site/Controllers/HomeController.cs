using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;
using System.Diagnostics;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("")]
public class HomeController : Controller
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IMontageService _montageService;
    private ILogger<HomeController> _logger;

    public HomeController(IUnitOfWork unitOfWork, IMapper mapper, IMontageService montageService, ILogger<HomeController> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _montageService = montageService;
        _logger = logger;   
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        System.Diagnostics.Trace.TraceInformation("My message!");
        Console.Write("MY TEST MESSAGE");
		_logger.LogInformation("HOME PAGE");
        return View(await _montageService.GetMontageAsync());
    }
 
    public IActionResult Privacy()
    {
        return View();
    }
     
    [HttpGet("error")]
    public IActionResult Error()
    {
        return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }     
}
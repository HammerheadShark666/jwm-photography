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

    public HomeController(IUnitOfWork unitOfWork, IMapper mapper, IMontageService montageService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _montageService = montageService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {                  
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
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("gallery")]
public class GalleryController : BaseController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IGalleryService _galleryService;
    private IUserGalleryService _userGalleryService;
    private ILogger<GalleryController> _logger;

    public GalleryController(IUnitOfWork unitOfWork,     
                             ILogger<GalleryController> logger,
                             IMapper mapper, 
                             IGalleryService galleryService, 
                             IUserGalleryService userGalleryService, 
                             IUserService userService) : base(userService)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _galleryService = galleryService;
        _userGalleryService = userGalleryService;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "id" })]
    public async Task<IActionResult> Gallery(int id)
    {
        var iteracion = 4;

        _logger.LogDebug($"Debug {iteracion}");
        _logger.LogInformation($"Information {iteracion}");
        _logger.LogWarning($"Warning {iteracion}");
        _logger.LogError($"Error {iteracion}");
        _logger.LogCritical($"Critical {iteracion}");

        return !IsLoggedIn()
            ? View("Gallery", await _galleryService.GetGalleryAsync(id))
            : View("Gallery", await _galleryService.GetGalleryAsync(GetUserId(), id)); 
    }
  
    [Authorize(Roles = "User, Admin")]
	[HttpGet("user/{id}")]
    [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "id" })]
    public async Task<IActionResult> UserGallery(int id)
	{ 
        return View("UserGallery", await _userGalleryService.GetUserGalleryAsync(GetUserId(), id));
	}
}
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

    public GalleryController(IUnitOfWork unitOfWork, 
                             IMapper mapper, 
                             IGalleryService galleryService, 
                             IUserGalleryService userGalleryService, 
                             IUserService userService) : base(userService)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _galleryService = galleryService;
        _userGalleryService = userGalleryService;
    }

    [HttpGet("{id}")]
    [ResponseCache(Duration = 300, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "id" })]
    public async Task<IActionResult> Gallery(int id)
    {
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
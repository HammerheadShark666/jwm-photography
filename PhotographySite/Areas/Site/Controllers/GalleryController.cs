using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("gallery")]
public class GalleryController : BaseController
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    private IGalleryService _galleryService;
    private IUserGalleryService _userGalleryService;

    public GalleryController(IUnitOfWork unitOfWork, IMapper mapper, IGalleryService galleryService, IUserGalleryService userGalleryService, IUserService userService) : base(userService)
	{
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _galleryService = galleryService;
        _userGalleryService = userGalleryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Gallery(int id)
    {
        Guid userId = GetUserId(HttpContext.User.Identity.Name);

        return View("Gallery", await _galleryService.GetGalleryAsync(userId, id));
    }

	[Authorize(Roles = "User, Admin")]
	[HttpGet("user/{id}")]
	public async Task<IActionResult> UserGallery(int id)
	{
		Guid userId = GetUserId(HttpContext.User.Identity.Name);

		return View("UserGallery", await _userGalleryService.GetUserGalleryAsync(userId,id));
	}
}

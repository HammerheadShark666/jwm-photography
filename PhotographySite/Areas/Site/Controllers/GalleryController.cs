using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Services.Interfaces;
using SwanSong.Service.Helpers.Exceptions;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("gallery")]
public class GalleryController : BaseController
{
    private IGalleryService _galleryService;
    private IUserGalleryService _userGalleryService;

    public GalleryController(IGalleryService galleryService,
                             IUserGalleryService userGalleryService,
                             IUserService userService) : base(userService)
    {
        _galleryService = galleryService;
        _userGalleryService = userGalleryService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Gallery(int id)
    {
        try
        {
            return !IsLoggedIn()
            ? View("Gallery", await _galleryService.GetGalleryAsync(id))
            : View("Gallery", await _galleryService.GetGalleryAsync(GetUserId(), id));
        }
        catch (GalleryNotFoundException gnfe)
        {
            return View("~/Views/Error/NotFound.cshtml", gnfe.Message);
        }
    }

    [Authorize(Roles = "User, Admin")]
    [HttpGet("user/{id}")]
    [ResponseCache(Duration = 300, VaryByQueryKeys = new string[] { "id" })]
    public async Task<IActionResult> UserGallery(int id)
    {
        try
        {
            return View("UserGallery", await _userGalleryService.GetUserGalleryAsync(GetUserId(), id));
        }
        catch (UserGalleryNotFoundException ugnfe)
        {
            return View("~/Views/Error/NotFound.cshtml", ugnfe.Message);
        }
    }
}
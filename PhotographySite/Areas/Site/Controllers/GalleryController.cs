using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Services.Interfaces;
using SwanSong.Service.Helpers.Exceptions;

namespace PhotographySite.Areas.Site.Controllers;

[Area("site")]
[Route("gallery")]
public class GalleryController(IGalleryService galleryService,
                         IUserGalleryService userGalleryService,
                         IUserService userService) : BaseController(userService)
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Gallery(int id)
    {
        try
        {
            return !IsLoggedIn()
            ? View("Gallery", await galleryService.GetGalleryAsync(id))
            : View("Gallery", await galleryService.GetGalleryAsync(GetUserId(), id));
        }
        catch (GalleryNotFoundException gnfe)
        {
            return View("~/Views/Error/NotFound.cshtml", gnfe.Message);
        }
    }

    [Authorize(Roles = "User, Admin")]
    [HttpGet("user/{id}")]
    [ResponseCache(Duration = 300, VaryByQueryKeys = ["id"])]
    public async Task<IActionResult> UserGallery(int id)
    {
        try
        {
            return View("UserGallery", await userGalleryService.GetUserGalleryAsync(GetUserId(), id));
        }
        catch (UserGalleryNotFoundException ugnfe)
        {
            return View("~/Views/Error/NotFound.cshtml", ugnfe.Message);
        }
    }
}
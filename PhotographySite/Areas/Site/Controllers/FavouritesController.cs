using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Dto.Request;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Authorize(Roles = "User, Admin")]
[Area("site")]
[Route("favourites")]
public class FavouritesController(IFavouriteService favouriteService,
                            IUserService userService) : BaseController(userService)
{
    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        IsValidUser();
        return View("Favourites", await favouriteService.AllAsync(GetUserId()));
    }

    [HttpPost("add-photo/{photoId}")]
    public async Task<IActionResult> AddPhoto(long photoId)
    {
        IsValidUser();
        return Ok(await favouriteService.AddAsync(new FavouriteAddRequest() { UserId = GetUserId(), PhotoId = photoId }));
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task DeletePhoto(long photoId)
    {
        IsValidUser();
        await favouriteService.DeleteAsync(GetUserId(), photoId);
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchPhotosAsync([FromBody] SearchPhotosRequest searchPhotosRequest)
    {
        IsValidUser();
        return Ok(await favouriteService.SearchPhotosAsync(GetUserId(), searchPhotosRequest));
    }
}
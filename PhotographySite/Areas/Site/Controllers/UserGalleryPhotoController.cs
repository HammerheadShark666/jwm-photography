using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Controllers;

[Authorize(Roles = "User, Admin")]
[Area("site")]
[Route("user/gallery-photos")]
public class UserGalleryPhotoController : BaseController
{
    private IPhotoCatalogService _photoCatalogService; 
    private IUserGalleryPhotoService _userGalleryPhotoService; 

    public UserGalleryPhotoController(IPhotoCatalogService photoCatalogService, IUserGalleryService userGalleryService, IUserGalleryPhotoService userGalleryPhotoService, IUserService userService) : base(userService)
    {
        _photoCatalogService = photoCatalogService;
        _userGalleryPhotoService = userGalleryPhotoService;
    } 

    [HttpPost("add")]
    public async Task<JsonResult> AddAsync([FromBody] UserGalleryPhotoDto userGalleryPhotoDto)
    {
        return new JsonResult(await _userGalleryPhotoService.AddPhotoToUserGalleryAsync(userGalleryPhotoDto));
    }

    [HttpPost("move")]
    public async Task<JsonResult> MoveAsync([FromBody] UserGalleryPhotoDto userGalleryPhotoDto)
    {
        return new JsonResult(await _userGalleryPhotoService.MovePhotoInGalleryAsync(userGalleryPhotoDto));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveAsync([FromBody] UserGalleryPhotoDto userGalleryPhotoDto)
    {
        await _userGalleryPhotoService.RemovePhotoFromGalleryAsync(userGalleryPhotoDto);
        return Ok();
    }
}
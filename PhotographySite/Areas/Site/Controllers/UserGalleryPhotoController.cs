using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Authorize(Roles = "User, Admin")]
[Area("site")]
[Route("user/gallery-photos")]
public class UserGalleryPhotoController : BaseController
{
    private IUserGalleryPhotoService _userGalleryPhotoService;

    public UserGalleryPhotoController(IUserGalleryPhotoService userGalleryPhotoService,
                                      IUserService userService) : base(userService)
    {
        _userGalleryPhotoService = userGalleryPhotoService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddAsync([FromBody] UserGalleryPhotoRequest userGalleryPhotoRequest)
    {
        IsValidUser();
        userGalleryPhotoRequest.UserId = GetUserId();
        return Ok(await _userGalleryPhotoService.AddPhotoToUserGalleryAsync(userGalleryPhotoRequest));
    }

    [HttpPost("move")]
    public async Task<IActionResult> MoveAsync([FromBody] UserGalleryMovePhotoRequest userGalleryMovePhotoRequest)
    {
        IsValidUser();
        userGalleryMovePhotoRequest.UserId = GetUserId();
        return Ok(await _userGalleryPhotoService.MovePhotoInGalleryAsync(userGalleryMovePhotoRequest));
    }

    [HttpPost("remove")]
    public async Task<IActionResult> RemoveAsync([FromBody] UserGalleryRemoveRequest userGalleryRemoveRequest)
    {
        IsValidUser();
        userGalleryRemoveRequest.UserId = GetUserId();
        await _userGalleryPhotoService.RemovePhotoFromGalleryAsync(userGalleryRemoveRequest);
        return Ok();
    }
}
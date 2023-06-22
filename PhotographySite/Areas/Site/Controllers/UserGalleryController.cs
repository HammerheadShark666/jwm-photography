using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Dtos;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Controllers;

[Authorize(Roles = "User, Admin")]
[Area("site")]
[Route("user/gallery")]
public class UserGalleryController : BaseController
{
    private IPhotoCatalogService _photoCatalogService;
    private IUserGalleryService _userGalleryService;
    private IUserGalleryPhotoService _userGalleryPhotoService; 

    public UserGalleryController(IPhotoCatalogService photoCatalogService, IUserGalleryService userGalleryService, IUserGalleryPhotoService userGalleryPhotoService, IUserService userService) : base(userService)
    {
        _photoCatalogService = photoCatalogService;
        _userGalleryService = userGalleryService;
        _userGalleryPhotoService = userGalleryPhotoService; 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Gallery(long id)
    {
		Guid userId = GetUserId(HttpContext.User.Identity.Name);

		UserGalleriesDto userGalleriesDto = new()
        {
            SelectedUserGallery = await _userGalleryService.GetUserGalleryAsync(userId, id),
            SelectUserGalleryPhotos = await _userGalleryPhotoService.GetGalleryPhotosAsync(id),
            LookupsDto = await _photoCatalogService.GetLookupsAsync(),
            UserGalleryListDto = await _userGalleryService.GetUserGalleriesAsync(HttpContext),
            PhotoListDto = await _photoCatalogService.GetPhotosPageAsync(new PhotoFilterDto()
            {
                PageIndex = 1,
                PageSize = 25,
            })
        };

        return View("UserGalleries", userGalleriesDto);
    } 

    [HttpPost("save/name")]
    public async Task<JsonResult> SaveName([FromBody] UserGalleryNameDto userGalleryNameDto)
    {
        Guid userId = GetUserId(HttpContext.User.Identity.Name);

        if (userId == Guid.Empty) 
            return NotAuthorised(userGalleryNameDto);  
          
        try
        {
            userGalleryNameDto.UserId = userId;
            userGalleryNameDto = await _userGalleryService.SaveName(userGalleryNameDto);
            Response.StatusCode = userGalleryNameDto.IsValid ? 200 : 400;
        }
        catch(UnauthorizedAccessException ex)
        {
            Response.StatusCode = 401;
            return new JsonResult(new { message = ex.Message });
        } 
        
        return new JsonResult(userGalleryNameDto);       
    }

    [HttpPost("new/save")]
    public async Task<JsonResult> SaveNewGallery([FromBody] UserGalleryNameDto userGalleryNameDto)
    { 
		Guid userId = GetUserId(HttpContext.User.Identity.Name);

		if (userId != Guid.Empty)
        {
			userGalleryNameDto.UserId = userId;
			userGalleryNameDto = await _userGalleryService.SaveNewUserGalleryAsync(userGalleryNameDto);

			Response.StatusCode = userGalleryNameDto.IsValid ? 200 : 400;
			return new JsonResult(userGalleryNameDto);
		}

        Response.StatusCode = 403;
		return new JsonResult(userGalleryNameDto);
	}

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUserGallery(long id)
    {
        Guid userId = GetUserId(HttpContext.User.Identity.Name);

        if (userId != Guid.Empty)
        {
            await _userGalleryService.DeleteAsync(id);
            return Ok();

        }

        Response.StatusCode = 400;
        return new JsonResult(new { message = "Not authorised to delete" });
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Admin.Services.Interfaces;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Dto.Response;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Helpers.Exceptions;
using PhotographySite.Services.Interfaces;
using SwanSong.Service.Helpers.Exceptions;

namespace PhotographySite.Areas.Site.Controllers;

[Authorize(Roles = "User, Admin")]
[Area("site")]
[Route("user/gallery")]
public class UserGalleryController : BaseController
{
    private IPhotoCatalogService _photoCatalogService;
    private IUserGalleryService _userGalleryService;
    private IUserGalleryPhotoService _userGalleryPhotoService; 

    public UserGalleryController(IPhotoCatalogService photoCatalogService, 
                                 IUserGalleryService userGalleryService, 
                                 IUserGalleryPhotoService userGalleryPhotoService, 
                                 IUserService userService) : base(userService)
    {
        _photoCatalogService = photoCatalogService;
        _userGalleryService = userGalleryService;
        _userGalleryPhotoService = userGalleryPhotoService; 
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Gallery(long id)
    {
        IsValidUser();

        try
        {
            return View("UserGalleries", await _userGalleryService.GetUserGalleryToEditAsync(GetUserId(), id));
        }
        catch (UserGalleryNotFoundException ugnfe)
        {
            return View("~/Views/Error/NotFound.cshtml", ugnfe.Message);
        }  
    }

    [HttpPost("add")]
    public async Task<ActionResult<UserGalleryActionResponse>>  Add([FromBody] UserGalleryAddRequest userGalleryAddRequest)
    {
        try
        {
            IsValidUser();
            userGalleryAddRequest.UserId = GetUserId();         
            return Ok(await _userGalleryService.AddAsync(userGalleryAddRequest));
        }
        catch (FailedValidationException fve)
        {
            return BadRequest(fve.FailedValidationResponse);
        }
        catch (Exception ex)
        { 
            return Problem(ex.Message);
        } 
    }

    [HttpPost("update")]
    public async Task<ActionResult<UserGalleryActionResponse>> Update([FromBody] UserGalleryUpdateRequest userGalleryUpdateRequest)
    {
        try
        {
            IsValidUser();
            userGalleryUpdateRequest.UserId = GetUserId();
            return Ok(await _userGalleryService.UpdateAsync(userGalleryUpdateRequest));
        }
        catch (FailedValidationException fve)
        {
            return BadRequest(fve.FailedValidationResponse);
        }
        catch (Exception ex)
        { 
            return Problem(ex.Message);
        } 
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUserGallery(long id)
    {
        try
        {
            IsValidUser();
            return Ok(await _userGalleryService.DeleteAsync(GetUserId(), id));
        }
        catch (UserGalleryNotFoundException cnfe)
        {
            return NotFound(cnfe.Message);
        }
        catch (FailedValidationException fve)
        {
            return BadRequest(fve.FailedValidationResponse);
        }
        catch (Exception ex)
        { 
            return Problem(ex.Message);
        } 
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Dto.Request;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Dto.Request;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

[Authorize(Roles = "User, Admin")]
[Area("site")]
[Route("favourites")]
public class FavouritesController : BaseController
{
    private IUnitOfWork _unitOfWork; 
	private IFavouriteService _favouriteService;
	private ILogger<FavouritesController> _logger; 

	public FavouritesController(IUnitOfWork unitOfWork, 
                                IFavouriteService favouriteService, 
                                ILogger<FavouritesController> logger, 
                                IUserService userService) : base(userService)
	{
		_unitOfWork = unitOfWork;
		_favouriteService = favouriteService;
		_logger = logger;
	}

	[HttpGet("")]
	public async Task<IActionResult> Index()
	{
        IsValidUser();  
		return View("Favourites", await _favouriteService.AllAsync(GetUserId()));
	}

    [HttpPost("add-photo/{photoId}")]
	public async Task<IActionResult> AddPhoto(long photoId)
    {
        IsValidUser();         
        return Ok(await _favouriteService.AddAsync(new FavouriteAddRequest() { UserId = GetUserId(), PhotoId = photoId })); 
    }

    [HttpDelete("delete-photo/{photoId}")]
    public async Task DeletePhoto(long photoId)
    {
        IsValidUser();         
        await _favouriteService.DeleteAsync(GetUserId(), photoId);
    }

    [HttpPost("search")]
    public async Task<IActionResult> SearchPhotosAsync([FromBody] SearchPhotosRequest searchPhotosRequest)
    { 
        IsValidUser(); 
        return Ok(await _favouriteService.SearchPhotosAsync(GetUserId(), searchPhotosRequest));
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Controllers;

[Authorize(Roles = "User, Admin")]
[Area("site")]
[Route("favourites")]
public class FavouritesController : BaseController
{
	private IUnitOfWork _unitOfWork; 
	private IFavouriteService _favouriteService;
	private ILogger<FavouritesController> _logger; 

	public FavouritesController(IUnitOfWork unitOfWork, IFavouriteService favouriteService, ILogger<FavouritesController> logger, IUserService userService) : base(userService)
	{
		_unitOfWork = unitOfWork;
		_favouriteService = favouriteService;
		_logger = logger;
	}

	[HttpGet("")]
	public async Task<IActionResult> Index()
	{ 
		FavouritesDto favourites = null;
        Guid userId = _unitOfWork.Users.GetUserIdAsync(HttpContext.User.Identity.Name);

		if(userId != Guid.Empty) 
            favourites = await _favouriteService.AllAsync(userId);

		return View("Favourites", favourites);
	}

    [HttpPost("add-photo/{photoId}")]
    public async Task AddPhoto(long photoId)
    { 
        Guid userId = _unitOfWork.Users.GetUserIdAsync(HttpContext.User.Identity.Name);

		if(userId.Equals(Guid.Empty))
			return;

		await _favouriteService.AddAsync(userId, photoId); 		 
    }

    [HttpPost("delete-photo/{photoId}")]
    public async Task DeletePhoto(long photoId)
    {
        Guid userId = GetUserId(HttpContext.User.Identity.Name);

        if (userId.Equals(Guid.Empty))
            return;

        await _favouriteService.DeleteAsync(userId, photoId);
    }

    [HttpPost("search")]
    public async Task<JsonResult> SearchPhotosAsync([FromBody] SearchPhotosDto searchPhotosDto)
    {
        Guid userId = GetUserId(HttpContext.User.Identity.Name);

        if (userId.Equals(Guid.Empty))
            return null;

        return new JsonResult(await _favouriteService.SearchPhotosAsync(userId, searchPhotosDto));
    }
}
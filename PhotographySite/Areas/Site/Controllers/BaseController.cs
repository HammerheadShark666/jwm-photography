using Microsoft.AspNetCore.Mvc;
using PhotographySite.Services.Interfaces;
using System.Security.Authentication;

namespace PhotographySite.Areas.Site.Controllers;

public class BaseController : Controller
{
	private IUserService _userService;

	public BaseController(IUserService userService)
	{
		_userService = userService;
	} 
  
	public void IsValidUser()
	{ 
        if((!User.Identity.IsAuthenticated) || (!User.IsInRole("User") && !User.IsInRole("Admin"))) 
            throw new AuthenticationException();
    }  

    public Guid GetUserId()
    {
        Guid userId = _userService.GetUserIdAsync(HttpContext.User.Identity.Name);
        if (userId.Equals(Guid.Empty))
            throw new AuthenticationException();

        return userId;
    }

    public bool IsLoggedIn()
    {
        Guid userId = _userService.GetUserIdAsync(HttpContext.User.Identity.Name);
        return userId.Equals(Guid.Empty) ? false : true; 
    }
}
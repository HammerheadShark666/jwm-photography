using Microsoft.AspNetCore.Mvc;
using PhotographySite.Areas.Site.Services.Interfaces;

namespace PhotographySite.Areas.Site.Controllers;

public class BaseController : Controller
{
	private IUserService _userService;

	public BaseController(IUserService userService)
	{
		_userService = userService;
	}

	public Guid GetUserId(string username)
	{
		return _userService.GetUserIdAsync(username);
	} 

	public JsonResult NotAuthorised(Object entity)
	{
        Response.StatusCode = 403;
        return new JsonResult(entity);
    }
}

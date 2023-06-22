using PhotographySite.Areas.Site.Services.Interfaces;

namespace PhotographySite.Areas.Site.Services;
public class BaseService
{
	private IUserService _userService; 

	public BaseService(IUserService userService)
	{
		_userService = userService;
	}

	public Guid GetUserId(string username)
	{
		return _userService.GetUserIdAsync(username);
	}
}

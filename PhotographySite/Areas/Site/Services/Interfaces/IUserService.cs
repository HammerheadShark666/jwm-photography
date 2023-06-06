namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IUserService
{
    Guid GetUserIdAsync(string email);
}

namespace PhotographySite.Services.Interfaces;

public interface IUserService
{
    Guid GetUserIdAsync(string email);
}
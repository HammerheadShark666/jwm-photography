namespace PhotographySite.Data.Repository.Interfaces;

public interface IUserRepository
{
    Guid GetUserIdAsync(string userName);
}
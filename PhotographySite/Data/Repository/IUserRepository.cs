namespace PhotographySite.Data.Repository;

public interface IUserRepository
{
    Guid GetUserIdAsync(string userName);
}

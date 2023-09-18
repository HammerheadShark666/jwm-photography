namespace PhotographySite.Data.Repository.Interfaces;

public interface IUserRepository
{
    Guid GetUserId(string userName);
}
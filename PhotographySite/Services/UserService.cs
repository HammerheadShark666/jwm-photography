using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Services;

public class UserService(IUnitOfWork unitOfWork) : IUserService
{
    public Guid GetUserIdAsync(string email)
    {
        return unitOfWork.Users.GetUserId(email);
    }
}
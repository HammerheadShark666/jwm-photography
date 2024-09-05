using PhotographySite.Data.Context;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class UserRepository(PhotographySiteDbContext context) : IUserRepository
{
    public Guid GetUserId(string userName)
    {
        ApplicationUser user = context.Users.FirstOrDefault(user => user.UserName == userName);

        if ((user != null) && (user.Id != null))
            return new Guid(user.Id);

        return Guid.Empty;
    }
}

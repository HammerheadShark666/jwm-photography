using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class UserRepository : IUserRepository
{
    private readonly PhotographySiteDbContext _context;

    public UserRepository(PhotographySiteDbContext context)
    {
        _context = context;
    }

    public Guid GetUserId(string userName)
    {
        ApplicationUser user = _context.Users.FirstOrDefault(user => user.UserName == userName);

        if ((user != null) && (user.Id != null))
            return new Guid(user.Id);

        return Guid.Empty;
    }
}

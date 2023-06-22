using AutoMapper;
using PhotographySite.Data.Contexts;
using PhotographySite.Data.Repository.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Data.Repository;

public class UserRepository :BaseRepository<ApplicationUser>, IUserRepository
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public UserRepository(PhotographySiteDbContext context) : base(context) { }

    public Guid GetUserIdAsync(string userName)
    {
        ApplicationUser user = _context.Users.FirstOrDefault(user => user.UserName == userName);

        if((user != null) && (user.Id != null))            
            return new Guid(user.Id);         

        return Guid.Empty;
    }
}

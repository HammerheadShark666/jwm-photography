using AutoMapper;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;

namespace PhotographySite.Areas.Site.Services;

public class UserService : IUserService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper; 

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)  
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper; 
    }

    public Guid GetUserIdAsync(string email)
    {
        return _unitOfWork.Users.GetUserIdAsync(email);
    }

}

using AutoMapper;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Services.Interfaces;

namespace PhotographySite.Services;

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
        return _unitOfWork.Users.GetUserId(email);
    }
}
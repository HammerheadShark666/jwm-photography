using AutoMapper;
using FluentValidation;
using PhotographySite.Areas.Site.Services.Interfaces;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Areas.Site.Services;

public class UserService : IUserService
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;
    //private IValidator<Gallery> _galleryValidator;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper) //, IValidator<Gallery> galleryValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        //_galleryValidator = galleryValidator;
    }

    public Guid GetUserIdAsync(string email)
    {
        return _unitOfWork.Users.GetUserIdAsync(email);
    }

}

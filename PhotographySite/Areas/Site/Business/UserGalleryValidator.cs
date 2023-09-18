using FluentValidation;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Areas.Site.Business;

public class UserGalleryValidator : AbstractValidator<UserGallery>
{       
    private readonly IUnitOfWork _unitOfWork;

    public UserGalleryValidator(IUnitOfWork unitOfWork)
    {   
        _unitOfWork= unitOfWork;    

        RuleSet("BeforeSave", () =>
        {
            RuleFor(userGallery => userGallery)
                .NotEmpty().WithMessage("User gallery not found.");

            RuleFor(userGallery => userGallery.Name)
                .NotEmpty().WithMessage("User gallery name is required.")
                .Length(1, 150).WithMessage("User gallery name must have a length between 1 and 150.");

            RuleFor(userGallery => userGallery).MustAsync(async (userGallery, cancellation) =>
            {
                return await UserGalleryNameExists(userGallery);
            }).WithMessage(gallery => $"The user gallery '{gallery.Name}' already exists.");
        });                
    }

    protected async Task<bool> UserGalleryNameExists(UserGallery userGallery)
    { 
        return userGallery.Id == 0
            ? !(await _unitOfWork.UserGalleries.ExistsAsync(userGallery.UserId, userGallery.Name))
            : !(await _unitOfWork.UserGalleries.ExistsAsync(userGallery.UserId, userGallery.Id, userGallery.Name));
    } 
}
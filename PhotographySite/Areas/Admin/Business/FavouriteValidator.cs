using FluentValidation;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Business;

public class FavouriteValidator : AbstractValidator<Favourite>
{       
    private readonly IUnitOfWork _unitOfWork;

    public FavouriteValidator(IUnitOfWork unitOfWork)
    {   
        _unitOfWork= unitOfWork;    

        RuleSet("BeforeSave", () =>
        { 
            RuleFor(favourite => favourite).MustAsync(async (favourite, cancellation) =>
            {
                return await PhotoAlreadyAFavourite(favourite);
            }).WithMessage("The photo is already a favourite.");
        });

        RuleSet("AfterSave", () => {

            RuleFor(favourite => favourite)
                .Null()
                .WithSeverity(Severity.Info) 
                .WithMessage("Photo added to favourite.");
        });
    } 

    protected async Task<bool> PhotoAlreadyAFavourite(Favourite favourite)
    { 
        return !(await _unitOfWork.Favourites.PhotoIsAlreadyAFavourite(favourite));
    }
}
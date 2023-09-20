using FluentValidation;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Areas.Admin.Business;

public class AdminGalleryValidator : AbstractValidator<Gallery>
{       
    private readonly IUnitOfWork _unitOfWork;

    public AdminGalleryValidator(IUnitOfWork unitOfWork)
    {   
        _unitOfWork= unitOfWork;    

        RuleSet("BeforeSave", () =>
        {              
            RuleFor(gallery => gallery.Name)
                .NotEmpty().WithMessage("Gallery name is required.")
                .Length(1, 150).WithMessage("Gallery name must have a length between 1 and 150.");

            RuleFor(gallery => gallery).MustAsync(async (gallery, cancellation) =>
            {
                return await GalleryNameExists(gallery);
            }).WithMessage(gallery => $"The gallery '{gallery.Name}' already exists.");
        });                
    }

    protected async Task<bool> GalleryNameExists(Gallery gallery)
    { 
        return gallery.Id == 0
            ? !(await _unitOfWork.Galleries.ExistsAsync(gallery.Name))
            : !(await _unitOfWork.Galleries.ExistsAsync(gallery.Id, gallery.Name));
    }
}
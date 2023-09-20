using FluentValidation;
using PhotographySite.Data.UnitOfWork.Interfaces;
using PhotographySite.Models;

namespace PhotographySite.Areas.Site.Business;

public class SiteGalleryValidator : AbstractValidator<Gallery>
{       
    private readonly IUnitOfWork _unitOfWork;

    public SiteGalleryValidator(IUnitOfWork unitOfWork)
    {   
        _unitOfWork= unitOfWork;    

        RuleSet("RecordExists", () =>
        {  
            RuleFor(gallery => gallery).MustAsync(async (gallery, cancellation) =>
            {
                return await GalleryExists(gallery.Id);
            }).WithMessage(gallery => $"Gallery not found.");
        });                
    }

    protected async Task<bool> GalleryExists(long id)
    {
        return (await _unitOfWork.Galleries.ByIdAsync(id) != null);
    } 
}
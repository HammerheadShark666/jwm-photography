using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Dto.Response;

public class FavouriteActionResponse : BaseResponse
{
    public long Id { get; set; }

    public FavouriteActionResponse() { }

    public FavouriteActionResponse(FluentValidation.Results.ValidationResult result)
    {
        Rules = result.Errors;
    }
}
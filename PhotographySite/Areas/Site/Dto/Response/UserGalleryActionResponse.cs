using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Dto.Response;

public class UserGalleryActionResponse : BaseResponse
{
    public long Id { get; set; }

    public UserGalleryActionResponse() { }

    public UserGalleryActionResponse(long id, FluentValidation.Results.ValidationResult result)
    {
        Id = id;
        Rules = result.Errors;
    }
}
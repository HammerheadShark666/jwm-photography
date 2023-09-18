using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Admin.Dto.Response;

public class GalleryActionResponse : BaseResponse
{
    public long Id { get; set; }

    public GalleryActionResponse() { }

    public GalleryActionResponse(long id, FluentValidation.Results.ValidationResult result)
    {
        Id = id;
        Rules = result.Errors;
    }
}
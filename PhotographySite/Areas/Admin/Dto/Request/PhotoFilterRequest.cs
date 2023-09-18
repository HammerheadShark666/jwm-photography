using PhotographySite.Dto.Request;

namespace PhotographySite.Areas.Admin.Dto.Request;

public class PhotoFilterRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string SortField { get; set; }
    public string SortOrder { get; set; }
    public long Id { get; set; }
    public string FileName { get; set; }
    public string Title { get; set; }
    public string Camera { get; set; }
    public string Lens { get; set; }
    public string ExposureTime { get; set; }
    public string AperturValue { get; set; }
    public int Iso { get; set; }
    public DateTime? DateTaken { get; set; }
    public string FocalLength { get; set; }
    public int CountryId { get; set; }
    public int CategoryId { get; set; }
    public int PaletteId { get; set; }

    public static PhotoFilterRequest Create(SearchPhotosRequest searchPhotosRequest)
    {
        return new PhotoFilterRequest()
        {
            Title = searchPhotosRequest.Title,
            CountryId = searchPhotosRequest.CountryId,
            CategoryId = searchPhotosRequest.CategoryId,
            PaletteId = searchPhotosRequest.PaletteId,
            PageIndex = searchPhotosRequest.PageIndex,
            PageSize = searchPhotosRequest.PageSize
        };
    }
}
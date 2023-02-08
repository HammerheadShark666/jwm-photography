namespace PhotographySite.Areas.Admin.Models;

public class SearchPhotosResultsDto
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int NumberOfPages { get; set; }
    public List<PhotoDto> Photos { get; set; }
    public int NumberOfPhotos { get; set; }

}

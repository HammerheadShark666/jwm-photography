namespace PhotographySite.Dto.Request;

public class SearchPhotosRequest
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string Title { get; set; }
    public int CountryId { get; set; }
    public int CategoryId { get; set; }
    public int PaletteId { get; set; }
}
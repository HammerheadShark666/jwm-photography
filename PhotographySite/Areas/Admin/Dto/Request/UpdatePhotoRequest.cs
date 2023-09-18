namespace PhotographySite.Areas.Admin.Dto.Request;

public class UpdatePhotoRequest
{ 
    public long Id { get; set; }
    public string Title { get; set; }
    public int CountryId { get; set; }
    public int CategoryId { get; set; }
    public int PaletteId { get; set; }
}
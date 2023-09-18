namespace PhotographySite.Areas.Admin.Dto.Request;

public class ExistingPhotoRequest
{
    public string FileName { get; set; }
    public string Title { get; set; }
    public DateTime? DateTaken { get; set; }
}
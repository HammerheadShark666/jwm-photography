namespace PhotographySite.Areas.Site.Dto.Response;

public class FavouriteResponse
{
    public Guid UserId { get; set; }
    public long PhotoId { get; set; }
    public string Title { get; set; }
    public string FileName { get; set; }
    public string Country { get; set; }

    public int Orientation
    {
        get; set;
    }
}
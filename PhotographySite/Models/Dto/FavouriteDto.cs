namespace PhotographySite.Models.Dto;

public class FavouriteDto
{ 
    public long PhotoId { get; set; }     

    public string Title { get; set; }

    public string FileName { get; set; }

    public string Country { get; set; }

    public int Orientation { get; set; }
}

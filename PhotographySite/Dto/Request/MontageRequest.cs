namespace PhotographySite.Dto.Request;

public class MontageRequest
{
    public int Id { get; set; }
    public long PhotoId { get; set; }
    public string Path { get; set; }
    public string Title { get; set; }
    public int Column { get; set; }
    public int Order { get; set; }
    public int Orientation { get; set; }
    public bool IsFavourite { get; set; }
}
using Newtonsoft.Json;

namespace PhotographySite.Models.Dto;

public class MontageDto
{
    [JsonProperty("id")]
    public int Id { get; set; }

    public long PhotoId { get; set; }

    [JsonProperty("path")]
    public string Path { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("column")]
    public int Column { get; set; }

    [JsonProperty("order")]
    public int Order { get; set; }

    [JsonProperty("orientation")]
    public int Orientation { get; set; }

    public bool IsFavourite { get; set; }
}

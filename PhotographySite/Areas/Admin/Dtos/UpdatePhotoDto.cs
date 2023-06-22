using Newtonsoft.Json;

namespace PhotographySite.Areas.Admin.Dtos;

public class UpdatePhotoDto
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("countryId")]
    public int CountryId { get; set; }

    [JsonProperty("categoryId")]
    public int CategoryId { get; set; }

    [JsonProperty("paletteId")]
    public int PaletteId { get; set; } 
}

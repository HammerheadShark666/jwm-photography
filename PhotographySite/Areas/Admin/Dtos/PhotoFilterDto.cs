using Newtonsoft.Json;

namespace PhotographySite.Areas.Admin.Dtos;

public class PhotoFilterDto
{
    [JsonProperty("pageIndex")]
    public int PageIndex { get; set; }

    [JsonProperty("pageSize")]
    public int PageSize { get; set; }

    [JsonProperty("sortField")]
    public string SortField { get; set; }

    [JsonProperty("sortOrder")]
    public string SortOrder { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("fileName")]
    public string FileName { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("camera")]
    public string Camera { get; set; }

    [JsonProperty("lens")]
    public string Lens { get; set; }

    [JsonProperty("exposureTime")]
    public string ExposureTime { get; set; }

    [JsonProperty("aperturValue")]
    public string AperturValue { get; set; }

    //[JsonProperty("exposureProgram")]
    //public string ExposureProgram { get; set; }

    [JsonProperty("iso")]
    public int Iso { get; set; }

    [JsonProperty("dateTaken")]
    public DateTime? DateTaken { get; set; }

    [JsonProperty("focalLength")]
    public string FocalLength { get; set; }

    [JsonProperty("countryId")]
    public int CountryId { get; set; }

    [JsonProperty("categoryId")]
    public int CategoryId { get; set; }

    [JsonProperty("paletteId")]
    public int PaletteId { get; set; }

    //public int Orientation { get; set; }

    //public int Height { get; set; }

    //public int Width { get; set; }

    //public Boolean UseInMontage { get; set; }
}

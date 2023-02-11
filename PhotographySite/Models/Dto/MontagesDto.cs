namespace PhotographySite.Models.Dto;

public class MontagesDto
{
    public List<List<MontageDto>> MontageImagesColumns { get; set; } = new List<List<MontageDto>>();

	public string AzureStoragePath { get; set; }
}

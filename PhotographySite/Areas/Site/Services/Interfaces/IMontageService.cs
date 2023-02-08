using PhotographySite.Models.Dto;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IMontageService
{
    Task<MontagesDto> GetMontageAsync();
}

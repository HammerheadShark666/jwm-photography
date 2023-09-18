using PhotographySite.Dto.Response;

namespace PhotographySite.Areas.Site.Services.Interfaces;

public interface IMontageService
{
    Task<MontagesResponse> GetMontageAsync(string username);
}